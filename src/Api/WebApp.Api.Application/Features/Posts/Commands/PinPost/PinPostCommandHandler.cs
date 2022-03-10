using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Telegram.Bot.Types.Enums;
using WebApp.Api.Application.Contracts.Infrastructure;
using WebApp.Api.Application.Contracts.Persistence;
using WebApp.Api.Application.Exceptions;
using WebApp.Api.Domain.Entities;

namespace WebApp.Api.Application.Features.Posts.Commands.PinPost
{
    public class PinPostCommandHandler : IRequestHandler<PinPostCommand>
    {
        private readonly IPostRepository _postRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IInstagramService _instagramService;
        private readonly ITelegramService _telegramService;

        public PinPostCommandHandler(IPostRepository postRepository, 
            IProjectRepository projectRepository,
            IInstagramService instagramService, 
            ITelegramService telegramService)
        {
            _postRepository = postRepository;
            _projectRepository = projectRepository;
            _instagramService = instagramService;
            _telegramService = telegramService;
        }

        public async Task<Unit> Handle(PinPostCommand request, CancellationToken cancellationToken)
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
            //            await _mediaPostService.UploadMediaAsync(project, post, true);
            //    }
            //}
            
            //publish telegram message
            var postMessageText = $"{post.Body}\n{post.Tags}\n@comedy_story";
            var message = await _telegramService.SendMessageAsync("@comedy_story", postMessageText, ParseMode.Html);
            await _telegramService.PinChatMessageAsync("@comedy_story", message.MessageId);

            return Unit.Value;
        }
    }
}
