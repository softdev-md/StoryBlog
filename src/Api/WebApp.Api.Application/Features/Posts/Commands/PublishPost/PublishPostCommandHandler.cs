using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WebApp.Api.Application.Contracts.Infrastructure;
using WebApp.Api.Application.Contracts.Persistence;
using WebApp.Api.Application.Exceptions;
using WebApp.Api.Domain.Entities;

namespace WebApp.Api.Application.Features.Posts.Commands.PublishPost
{
    public class PublishPostCommandHandler : IRequestHandler<PublishPostCommand>
    {
        private readonly IPostRepository _postRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IInstagramService _instagramService;
        private readonly ITelegramService _telegramService;

        public PublishPostCommandHandler(IPostRepository postRepository, 
            IProjectRepository projectRepository, 
            IInstagramService instagramService, 
            ITelegramService telegramService)
        {
            _postRepository = postRepository;
            _projectRepository = projectRepository;
            _instagramService = instagramService;
            _telegramService = telegramService;
        }

        public async Task<Unit> Handle(PublishPostCommand request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetByIdAsync(request.Id);
            if (post == null)
                throw new NotFoundException(nameof(Post), request.Id);
            
            //get project
            var project = await _projectRepository.GetByIdAsync(post.ProjectId);

            //prepare publish instagram post
            //var projectAccount = await _projectRepository.GetProjectAccountByProjectIdAsync(project.Id, (int)AccountType.Instagram);
            //if (projectAccount != null)
            //{
            //    var account = await _accountService.GetAccountByIdAsync(projectAccount.AccountId);
            //    if (account.AccountTypeId == (int)AccountType.Instagram)
            //    {
            //        var loginResult = await _instagramService.LogIn(project.Name, account.UserName, account.UserPassword);
            //        if (loginResult)
            //            await _mediaPostService.UploadMediaAsync(project, post);
            //    }
            //}

            //await _mediaPostService.UploadTelegramMediaAsync(project, post);
            
            //publish telegram message
            //var postMessageText = $"{post.Body}\n{post.Tags}\n@comedy_story";
            //await _telegramService.SendMessageAsync("@comedy_story", postMessageText, ParseMode.Html);

            ////update post
            //post.PostStatusId = (int)PostStatus.Published;
            //post.PublishedOn = DateTime.Now;
            //await _postRepository.UpdatePostAsync(post);
            
            return Unit.Value;
        }
    }
}
