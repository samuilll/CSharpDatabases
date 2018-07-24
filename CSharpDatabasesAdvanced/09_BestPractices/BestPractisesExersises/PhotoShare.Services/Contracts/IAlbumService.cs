using PhotoShare.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoShare.Services.Contracts
{
  public  interface IAlbumService
    {
        Album GetById(int Id);

        Album GetByName(string albumName);

        Album Create(string username, string albumTitle, string color, string[] tags);

        bool Exist(string title);

        void AddTagTo(string albumname,string tagname, User credentialUser);

        string ShareAlbum(int albumId,string username,string persmission,User credentialUser);

        string UploadPicture(string albumName, string pictureTitle, string filePath, User credentialUser);
    }
}
