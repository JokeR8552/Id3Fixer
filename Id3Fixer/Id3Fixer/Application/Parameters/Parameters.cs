namespace Id3Fixer.Application.Parameters
{
    internal class Parameters
    {
        public Parameters(string basePath, string playlistFileName)
        {
            BasePath = basePath;
            PlaylistFileName = playlistFileName;
        }

        public string BasePath { get; }
        public string PlaylistFileName { get; }
    }
}
