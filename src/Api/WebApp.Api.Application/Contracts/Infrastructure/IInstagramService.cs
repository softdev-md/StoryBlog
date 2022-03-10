using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using InstagramApiSharp.Classes.Models;
using InstagramApiSharp.Enums;
using WebApp.Api.Application.Models;

namespace WebApp.Api.Application.Contracts.Infrastructure
{
    /// <summary>
    /// Instagram service interface
    /// </summary>
    public interface IInstagramService
    {
        /// <summary>
        /// Log in account
        /// </summary>
        /// <param name="project">Project name</param>
        /// <param name="userName">Username</param>
        /// <param name="password">Password</param>
        Task<bool> LogIn(string project, string userName, string password);

        /// <summary>
        /// Upload media
        /// </summary>
        /// <param name="project">Project name</param>
        /// <param name="uploadMedia">Upload media</param>
        /// <param name="description">description</param>
        /// <param name="type">File type</param>
        /// <returns>Success upload result</returns>
        Task<bool> UploadMedia(string project, Image[] uploadMedia, string description,
            FileType type = FileType.Image);

        /// <summary>
        /// Upload story
        /// </summary>
        /// <param name="project">Project name</param>
        /// <param name="image">Upload image</param>
        /// <param name="description">description</param>
        /// <param name="options"></param>
        /// <param name="type">File type</param>
        /// <returns>Success upload result</returns>
        Task<bool> UploadStory(string project, Image image, string description,
            InstaStoryUploadOptions options = null, FileType type = FileType.Image);

        /// <summary>
        /// Get people
        /// </summary>
        /// <param name="project">Project name</param>
        /// <param name="query">Upload media</param>
        /// <param name="count">Count rows</param>
        /// <param name="follow"></param>
        /// <returns>Success people result</returns>
        Task<List<InstaUser>> SearchPeople(string project, string query, int count = 50, bool follow = false);

        /// <summary>
        /// Get top search users
        /// </summary>
        /// <param name="project">Project name</param>
        /// <param name="query">Upload media</param>
        /// <param name="searchType">Search type</param>
        /// <param name="count">Count rows</param>
        /// <param name="follow">Follow users</param>
        /// <returns>Success people result</returns>
        Task<List<InstaUser>> GetTopSearchUsers(string project, string query, InstaDiscoverSearchType searchType,
            int count = 50, bool follow = false);

        /// <summary>
        /// Get user 
        /// </summary>
        /// <param name="project">Project name</param>
        /// <param name="userId">User id</param>
        /// <returns>Success user followers result</returns>
        Task<InstaUserInfo> GetFullUserInfo(string project, long userId);

        /// <summary>
        /// Get user followers
        /// </summary>
        /// <param name="project">Project name</param>
        /// <param name="username">Upload media</param>
        /// <param name="follow"></param>
        /// <param name="maxPages"></param>
        /// <returns>Success user followers result</returns>
        Task<List<InstaUser>> GetUserFollowers(string project, string username, int maxPages = 0, bool follow = false);

        /// <summary>
        /// Get user followers
        /// </summary>
        /// <param name="project">Project name</param>
        /// <param name="username">Upload media</param>
        /// <param name="follow"></param>
        /// <param name="maxPages"></param>
        /// <returns>Success user followers result</returns>
        Task<List<InstaUser>> GetUserFollowersByActivity(string project, string username, int maxPages = 0, 
            DateTime? mdeiaPublishedFrom = null,
            bool follow = false);

        /// <summary>
        /// Follow user
        /// </summary>
        /// <param name="project">Project name</param>
        /// <param name="userId">User id</param>
        /// <returns>Success follow result</returns>
        Task<bool> FollowUser(string project, long userId);

        /// <summary>
        /// Un follow user
        /// </summary>
        /// <param name="project">Project name</param>
        /// <param name="userId">User id</param>
        /// <returns>Success un follow result</returns>
        Task<bool> UnFollowUser(string project, long userId);

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
        Task<bool> GetUserMedia(string project, string username,
            bool likeMedia = false, bool likeMediaComment = false, bool previewStory = false,
            string comment = null, int count = 0);

        /// <summary>
        /// Like media
        /// </summary>
        /// <param name="project">Project name</param>
        /// <param name="mediaId">Media id</param>
        /// <returns>Success liked result</returns>
        Task<bool> LikeUserMedia(string project, string mediaId);

        /// <summary>
        /// Comment media
        /// </summary>
        /// <param name="project">Project name</param>
        /// <param name="mediaId">Media id</param>
        /// <param name="text">Comment text</param>
        /// <returns>Success comment result</returns>
        Task<bool> CommentMedia(string project, string mediaId, string text);
    }
}
