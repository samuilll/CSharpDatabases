using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoShare.Services
{
    using Contracts;
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System.Linq;

    public class TagService : ITagService
    {
        private readonly PhotoShareContext context;

        public TagService(PhotoShareContext context)
        {
            this.context = context;
        }

        public Tag Create(string tagName)
        {
            var tag = context.Tags.SingleOrDefault(t=>t.Name=="#"+tagName);

            if (tag!=null)
            {
                throw new ArgumentException(string.Format(ExeptionMessageHandler.TagAlreadyExistExeption,tagName));
            }

            tag = new Tag()
            {
                Name ="#"+ tagName
            };

            this.context.Tags.Add(tag);

            this.context.SaveChanges();

            return tag;
        }

        public bool Exist(string tagName)
        {
            return context.Tags.Any(t => t.Name ==tagName);
        }

        public Tag GetByName(string tagName)
        {
            var tag = context.Tags.SingleOrDefault(t=>t.Name==tagName);

            if (tag==null)
            {
                throw new ArgumentException(string.Format(ExeptionMessageHandler.TagDoesNotExist, tagName));
            }

            return tag;
        }
    }
}
