

using PhotoShare.Models;

namespace PhotoShare.Services.Contracts
{
    public interface ITagService
    {

        Tag GetByName(string tagName);

        Tag Create(string tagName);

        bool Exist(string tagName);
    }
}
