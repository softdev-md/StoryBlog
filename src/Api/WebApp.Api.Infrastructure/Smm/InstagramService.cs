using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using InstagramApiSharp;
using InstagramApiSharp.API;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Enums;
using InstagramApiSharp.Logger;
using Microsoft.Extensions.Logging;
using WebApp.Api.Application.Contracts.Infrastructure;
using WebApp.Api.Application.Contracts.Persistence;
using WebApp.Api.Application.Models;
using LogLevel = InstagramApiSharp.Logger.LogLevel;

namespace WebApp.Api.Infrastructure.Smm
{
    /// <summary>
    /// Instagram service
    /// </summary>
    public class InstagramService : IInstagramService
    {
        #region Fields
        
        private readonly ILogger<InstagramService> _logger;

        private readonly Dictionary<string, IInstaApi> _instaApiInstances;
        private readonly Dictionary<string, string> _instaApiSessions;

        #endregion

        #region Ctor

        public InstagramService(ILogger<InstagramService> logger)
        {
            _logger = logger;

            _instaApiInstances = new Dictionary<string, IInstaApi>();
            _instaApiSessions = new Dictionary<string, string>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Log in account
        /// </summary>
        /// <param name="project">Project name</param>
        /// <param name="userName">Username</param>
        /// <param name="password">Password</param>
        public async Task<bool> LogIn(string project, string userName, string password)
        {
            if (_instaApiInstances.ContainsKey(project) && 
                _instaApiInstances[project].IsUserAuthenticated)
                return true;

            var userSession = new UserSessionData
            {
                UserName = userName,
                Password = password
            };

            var instaApi = InstaApiBuilder.CreateBuilder()
                .SetUser(userSession)
                .UseLogger(new DebugLogger(LogLevel.Exceptions))
                .Build();

            var loginResult = await instaApi.LoginAsync();
            if (loginResult.Succeeded)
            {
                if (instaApi.IsUserAuthenticated)
                {
                    //
                    if (!_instaApiInstances.ContainsKey(project))
                        _instaApiInstances.Add(project, instaApi);
                    else
                        _instaApiInstances[project] = instaApi;

                    //
                    if (!_instaApiSessions.ContainsKey(project))
                        _instaApiSessions.Add(project, instaApi.GetStateDataAsString());
                    else
                        _instaApiSessions[project] = instaApi.GetStateDataAsString();
                }
            }
            else
            {
                switch (loginResult.Value)
                {
                    case InstaLoginResult.InvalidUser:
                        _logger.LogWarning("Username is invalid.");
                        break;
                    case InstaLoginResult.BadPassword:
                        _logger.LogWarning("Password is wrong.");
                        break;
                    case InstaLoginResult.Exception:
                        _logger.LogWarning("Exception throws:\n" + loginResult.Info?.Message);
                        break;
                    case InstaLoginResult.LimitError:
                        _logger.LogWarning("Limit error (you should wait 10 minutes).");
                        break;
                    case InstaLoginResult.ChallengeRequired:
                        break;
                    case InstaLoginResult.TwoFactorRequired:
                        break;
                }
            }

            return loginResult.Succeeded;
        }

        /// <summary>
        /// Upload media
        /// </summary>
        /// <param name="project">Project name</param>
        /// <param name="uploadMedia">Upload media</param>
        /// <param name="description">description</param>
        /// <param name="type">File type</param>
        /// <returns>Success upload result</returns>
        public async Task<bool> UploadMedia(string project, Image[] uploadMedia, string description, FileType type = FileType.Image)
        {
            if (uploadMedia.Length == 1)
            {
                // single photo or video
                var media = uploadMedia.FirstOrDefault();
                
                if (type == FileType.Video)
                {
                    //var screenshot = Path.Combine(Helper.CacheTempPath, Helper.RandomString()) + ".jpg";
                    //Helper.FFmpeg.ExtractImageFromVideo(firstPath, screenshot);

                    //await Task.Delay(500);
                    //var vid = new InstaVideoUpload
                    //{
                    //    Video = new InstaVideo(firstPath, 0, 0),
                    //    VideoThumbnail = new InstaImage(screenshot, 0, 0)
                    //};
                    //var up = await InstaApi.MediaProcessor.UploadVideoAsync(vid, caption);
                    //if (up.Succeeded)
                    //    "Your video uploaded successfully.".ShowMsg("Succeeded", MessageBoxImage.Information);
                    //else
                    //    up.Info.Message.ShowMsg("ERR", MessageBoxImage.Error);
                }
                else
                {
                    var image = new InstaImageUpload();

                    using (var memoryStream = new MemoryStream())
                    {
                        var encoderParameters = new EncoderParameters(1);
                        encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
                        var codec = ImageCodecInfo.GetImageDecoders().FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);

                        media.Save(memoryStream, codec, encoderParameters);
                        image.ImageBytes = memoryStream.ToArray();
                    }

                    if (_instaApiInstances[project] == null)
                        return false;

                    var uploadPhoto = await _instaApiInstances[project].MediaProcessor.UploadPhotoAsync(image, description);
                    if (!uploadPhoto.Succeeded)
                        _logger.LogWarning(uploadPhoto.Info.Message);

                    return uploadPhoto.Succeeded;
                }
            }
            else
            {
                // album
                var videos = new List<InstaVideoUpload>();
                var images = new List<InstaImageUpload>();

                foreach (var media in uploadMedia)
                {
                    if (type == FileType.Image)
                    {
                        var image = new InstaImageUpload();

                        using (var memoryStream = new MemoryStream())
                        {
                            var encoderParameters = new EncoderParameters(1);
                            encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
                            var codec = ImageCodecInfo.GetImageDecoders().FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);

                            media?.Save(memoryStream, codec, encoderParameters);
                            image.ImageBytes = memoryStream.ToArray();
                        }
                        images.Add(image);
                    }
                    else
                    {
                        //var screenshot = Path.Combine(Helper.CacheTempPath, Helper.RandomString()) + ".jpg";
                        //Helper.FFmpeg.ExtractImageFromVideo(item, screenshot);

                        //var vid = new InstaVideoUpload
                        //{
                        //    Video = new InstaVideo(item, 0, 0),
                        //    VideoThumbnail = new InstaImage(screenshot, 0, 0)
                        //};
                        //videos.Add(vid);
                        //await Task.Delay(2000);
                    }
                }
                //await Task.Delay(6000);

                if (_instaApiInstances[project] == null)
                    return false;

                var uploadAlbum = await _instaApiInstances[project].MediaProcessor.UploadAlbumAsync(images.ToArray(),
                    videos.ToArray(), description);

                if (!uploadAlbum.Succeeded)
                    _logger.LogWarning(uploadAlbum.Info.Message);

                return uploadAlbum.Succeeded;
            }

            return false;
        }

        /// <summary>
        /// Upload story
        /// </summary>
        /// <param name="project">Project name</param>
        /// <param name="image">Upload image</param>
        /// <param name="description">description</param>
        /// <param name="options"></param>
        /// <param name="type">File type</param>
        /// <returns>Success upload result</returns>
        public async Task<bool> UploadStory(string project, Image image, string description, InstaStoryUploadOptions options = null,  FileType type = FileType.Image)
        {
            //if (!InstaApi.IsUserAuthenticated)
            //    return;

            if (type == FileType.Video)
            {
                //var screenshot = Path.Combine(Helper.CacheTempPath, Helper.RandomString()) + ".jpg";
                //Helper.FFmpeg.ExtractImageFromVideo(firstPath, screenshot);

                //await Task.Delay(500);
                //var vid = new InstaVideoUpload
                //{
                //    Video = new InstaVideo(firstPath, 0, 0),
                //    VideoThumbnail = new InstaImage(screenshot, 0, 0)
                //};
                //var up = await InstaApi.MediaProcessor.UploadVideoAsync(vid, caption);
                //if (up.Succeeded)
                //    "Your video uploaded successfully.".ShowMsg("Succeeded", MessageBoxImage.Information);
                //else
                //    up.Info.Message.ShowMsg("ERR", MessageBoxImage.Error);
            }
            else
            {
                var media = new InstaImage();

                using (var memoryStream = new MemoryStream())
                {
                    var encoderParameters = new EncoderParameters(1);
                    encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
                    var codec = ImageCodecInfo.GetImageDecoders().FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);

                    image.Save(memoryStream, codec, encoderParameters);
                    media.ImageBytes = memoryStream.ToArray();
                }

                if (_instaApiInstances[project] == null)
                    return false;

                var uploadStoryPhoto = await _instaApiInstances[project].StoryProcessor.UploadStoryPhotoAsync(media, description, options);
                if (!uploadStoryPhoto.Succeeded)
                    _logger.LogWarning(uploadStoryPhoto.Info.Message);

                return uploadStoryPhoto.Succeeded;
            }

            return false;
        }

        /// <summary>
        /// Get people
        /// </summary>
        /// <param name="project">Project name</param>
        /// <param name="query">Upload media</param>
        /// <param name="count">Count rows</param>
        /// <param name="follow"></param>
        /// <returns>Success people result</returns>
        public async Task<List<InstaUser>> SearchPeople(string project, string query, int count = 50, bool follow = false)
        {
            if (_instaApiInstances[project] == null)
                return null;

            var people = await _instaApiInstances[project].DiscoverProcessor.SearchPeopleAsync(query, count);

            if (people.Succeeded && follow)
            {
                foreach (var userFollower in people.Value.Users)
                    await _instaApiInstances[project].UserProcessor.FollowUserAsync(userFollower.Pk);
            }

            return people.Succeeded ? people.Value.Users : null;
        }

        /// <summary>
        /// Get top search users
        /// </summary>
        /// <param name="project">Project name</param>
        /// <param name="query">Upload media</param>
        /// <param name="searchType">Search type</param>
        /// <param name="count">Count rows</param>
        /// <param name="follow">Follow users</param>
        /// <returns>Success people result</returns>
        public async Task<List<InstaUser>> GetTopSearchUsers(string project, string query, InstaDiscoverSearchType searchType, int count = 50, bool follow = false)
        {
            if (_instaApiInstances[project] == null)
                return null;

            var searchResult = await _instaApiInstances[project].DiscoverProcessor.GetTopSearchesAsync(query, searchType);

            if (searchResult.Succeeded && follow)
            {
                foreach (var userFollower in searchResult.Value.TopResults)
                    await _instaApiInstances[project].UserProcessor.FollowUserAsync(userFollower.User.Pk);
            }

            return searchResult.Succeeded ? searchResult.Value.TopResults.Select(s => s.User).ToList() : null;
        }

        /// <summary>
        /// Get user 
        /// </summary>
        /// <param name="project">Project name</param>
        /// <param name="userId">User id</param>
        /// <returns>Success user followers result</returns>
        public async Task<InstaUserInfo> GetFullUserInfo(string project, long userId)
        {
            if (_instaApiInstances[project] == null)
                return null;

            var user = await _instaApiInstances[project].UserProcessor.GetFullUserInfoAsync(userId);
            if (!user.Succeeded)
                return null;

            return user.Value.UserDetail;
        }

        /// <summary>
        /// Get user followers
        /// </summary>
        /// <param name="project">Project name</param>
        /// <param name="username">Upload media</param>
        /// <param name="follow"></param>
        /// <param name="maxPages"></param>
        /// <returns>Success user followers result</returns>
        public async Task<List<InstaUser>> GetUserFollowers(string project, string username, int maxPages = 0, bool follow = false)
        {
            if (_instaApiInstances[project] == null)
                return null;

            //var user = await _instaApiInstances[project].UserProcessor.GetUserAsync(username);
            //if (!user.Succeeded)
            //    _logger.LogWarning(user.Info.Message);

            var paginationParameters = maxPages > 0 ? PaginationParameters.MaxPagesToLoad(maxPages) : PaginationParameters.Empty;
            var userFollowers = await _instaApiInstances[project].UserProcessor.GetUserFollowersAsync(username, paginationParameters);
            if (!userFollowers.Succeeded)
                _logger.LogWarning(userFollowers.Info.Message);

            if (userFollowers.Succeeded && follow)
            {
                foreach (var userFollower in userFollowers.Value)
                    await _instaApiInstances[project].UserProcessor.FollowUserAsync(userFollower.Pk);
            }

            return userFollowers.Value?.Select(u => new InstaUser(u)).ToList();
        }

        /// <summary>
        /// Get user followers
        /// </summary>
        /// <param name="project">Project name</param>
        /// <param name="username">Upload media</param>
        /// <param name="mdeiaPublishedFrom"></param>
        /// <param name="follow"></param>
        /// <param name="maxPages"></param>
        /// <returns>Success user followers result</returns>
        public async Task<List<InstaUser>> GetUserFollowersByActivity(string project, string username, int maxPages = 0,
            DateTime? mdeiaPublishedFrom = null, bool follow = false)
        {
            if (_instaApiInstances[project] == null)
                return null;

            var user = await _instaApiInstances[project].UserProcessor.GetUserAsync(username);
            if (!user.Succeeded)
                _logger.LogWarning(user.Info.Message);

            var users = new List<InstaUserShort>();

            var paginationParameters = maxPages > 0 ? PaginationParameters.MaxPagesToLoad(maxPages) : PaginationParameters.Empty;
            var mediaList = await _instaApiInstances[project].UserProcessor.GetUserMediaAsync(username, paginationParameters);
            if (mediaList.Succeeded && mediaList.Value.Count > 0)
            {
                foreach (var media in mediaList.Value)
                {
                    //media likers
                    var mediaCommentLikers = await _instaApiInstances[project].CommentProcessor.GetMediaCommentLikersAsync(media.Pk);
                    if (mediaCommentLikers.Succeeded)
                    {
                        foreach (var mediaCommentLiker in mediaCommentLikers.Value)
                        {
                            users.Add(mediaCommentLiker);
                        }
                    }

                    await Task.Delay(1000);

                    //comments users
                    var mediaComments = await _instaApiInstances[project].CommentProcessor
                        .GetMediaCommentsAsync(media.Pk, PaginationParameters.Empty);
                    if (mediaComments.Succeeded)
                    {
                        foreach (var mediaComment in mediaComments.Value.Comments)
                        {
                            users.Add(mediaComment.User);
                        }
                    }
                }
            }

            //distinct users
            var usersResult = users.GroupBy(u => u.Pk).Select(group => new InstaUser(group.First())).ToList();

            //follow
            if (follow && usersResult.Count > 0)
            {
                foreach (var userFollower in usersResult)
                    await _instaApiInstances[project].UserProcessor.FollowUserAsync(userFollower.Pk);
            }

            return usersResult;
        }

        /// <summary>
        /// Follow user
        /// </summary>
        /// <param name="project">Project name</param>
        /// <param name="userId">User id</param>
        /// <returns>Success follow result</returns>
        public async Task<bool> FollowUser(string project, long userId)
        {
            if (_instaApiInstances[project] == null)
                return false;

            var result = await _instaApiInstances[project].UserProcessor.FollowUserAsync(userId);
            if (!result.Succeeded)
                _logger.LogWarning(result.Info.Message);

            return result.Succeeded;
        }

        /// <summary>
        /// Un follow user
        /// </summary>
        /// <param name="project">Project name</param>
        /// <param name="userId">User id</param>
        /// <returns>Success un follow result</returns>
        public async Task<bool> UnFollowUser(string project, long userId)
        {
            if (_instaApiInstances[project] == null)
                return false;

            var result = await _instaApiInstances[project].UserProcessor.UnFollowUserAsync(userId);
            if (!result.Succeeded)
                _logger.LogWarning(result.Info.Message);

            return result.Succeeded;
        }

        /// <summary>
        /// Get user media
        /// </summary>
        /// <param name="project">Project name</param>
        /// <param name="username">Upload media</param>
        /// <param name="likeMediaComment"></param>
        /// <param name="likeMedia"></param>
        /// <param name="previewStory"></param>
        /// <param name="comment">Comment</param>
        /// <param name="count"></param>
        /// <returns>Success user media result</returns>
        public async Task<bool> GetUserMedia(string project, string username, 
            bool likeMedia = false, bool likeMediaComment = false, bool previewStory = false,
            string comment = null, int count = 0)
        {
            if (_instaApiInstances[project] == null)
                return false;

            var user = await _instaApiInstances[project].UserProcessor.GetUserAsync(username);
            if (!user.Succeeded)
            {
                _logger.LogWarning(user.Info.Message);
                return false;
            }

            var mediaList = await _instaApiInstances[project].UserProcessor.GetUserMediaAsync(username, PaginationParameters.MaxPagesToLoad(1));
            if (mediaList.Succeeded && mediaList.Value.Count > 0 && (likeMedia || likeMediaComment || !string.IsNullOrEmpty(comment)))
            {
                var topMediaList =  mediaList.Value.Take(count);
                foreach (var media in topMediaList)
                {
                    if(likeMedia)
                        await _instaApiInstances[project].MediaProcessor.LikeMediaAsync(media.Pk);

                    if (likeMediaComment)
                    {
                        var mediaComments = await _instaApiInstances[project].CommentProcessor
                            .GetMediaCommentsAsync(media.Pk, PaginationParameters.MaxPagesToLoad(1));
                        foreach (var mediaComment in mediaComments.Value.Comments.Take(count))
                        {
                            await _instaApiInstances[project].CommentProcessor.LikeCommentAsync(mediaComment.Pk.ToString());
                        }
                    }
                }

                if (!string.IsNullOrEmpty(comment))
                    await _instaApiInstances[project].CommentProcessor.CommentMediaAsync(mediaList.Value.First().Pk, comment);
            }

            if (previewStory)
                await _instaApiInstances[project].StoryProcessor.GetUserStoryAsync(user.Value.Pk);

            return mediaList.Succeeded;
        }

        /// <summary>
        /// Like media
        /// </summary>
        /// <param name="project">Project name</param>
        /// <param name="mediaId">Media id</param>
        /// <returns>Success liked result</returns>
        public async Task<bool> LikeUserMedia(string project, string mediaId)
        {
            if (_instaApiInstances[project] == null)
                return false;

            var result = await _instaApiInstances[project].MediaProcessor.LikeMediaAsync(mediaId);
            if (!result.Succeeded)
                _logger.LogWarning(result.Info.Message);

            return result.Succeeded;
        }

        /// <summary>
        /// Comment media
        /// </summary>
        /// <param name="project">Project name</param>
        /// <param name="mediaId">Media id</param>
        /// <param name="text">Comment text</param>
        /// <returns>Success comment result</returns>
        public async Task<bool> CommentMedia(string project, string mediaId, string text)
        {
            if (_instaApiInstances[project] == null)
                return false;

            var result = await _instaApiInstances[project].CommentProcessor.CommentMediaAsync(mediaId, text);
            if (!result.Succeeded)
                _logger.LogWarning(result.Info.Message);

            return result.Succeeded;
        }

        #endregion
    }
}
