namespace Id3Fixer.Application.TagsFixer;

public interface ITagsFixer
{
    void FixTags(List<SongInfo> songInfos);
}
