using System;
using MusicApi.Backend;
using MusicApi.Backend.SourceApi.Database;

namespace MusicApiTest.Backend
{
    public static class Constants
    {
        public static readonly DBApi DB = new DBApi(BackendController.DEFAULT_DIRECTORY);
    }
}
