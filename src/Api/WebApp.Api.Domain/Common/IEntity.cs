using System;

namespace WebApp.Api.Domain.Common
{
    public interface IEntity<out TKey> where TKey : struct
    {
        TKey Id { get; }
    }

    public interface IEntity : IEntity<Guid>
    {
    }
}