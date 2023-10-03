using Id3;

namespace Id3Fixer.Test;

[TestFixture]
public class FixerProgramTests
{
    [Test]
    public void Main_Positive()
    {
        string basePath = Path.Combine(Environment.CurrentDirectory, "Files");
        string playlistFileName = "play.aimppl4";
        File.Replace(
            Path.Combine(basePath, "02-02-Дружок_bad_backup.mp3"),
            Path.Combine(basePath, "02-02-Дружок_work.mp3"), null);

        var mp3bad = new Mp3(Path.Combine(basePath, "02-02-Дружок_work.mp3"), Mp3Permissions.Read);
        Id3Tag tagsBad = mp3bad.GetTag(Id3TagFamily.Version2X);
        string badArtist = string.Join(' ', tagsBad.Artists.Value);
        string badAlbum = tagsBad.Album.Value;
        string badTitle = tagsBad.Title.Value;
        mp3bad.Dispose();

        Assert.DoesNotThrow(() => FixerProgram.Main(new string[] { basePath, playlistFileName }));

        var mp3work = new Mp3(Path.Combine(basePath, "02-02-Дружок_work.mp3"), Mp3Permissions.Read);
        var mp3good = new Mp3(Path.Combine(basePath, "02-02-Дружок_good.mp3"), Mp3Permissions.Read);
        Id3Tag tagsWork = mp3work.GetTag(Id3TagFamily.Version2X);
        Id3Tag tagsGood = mp3good.GetTag(Id3TagFamily.Version2X);
        Assert.Multiple(() =>
        {
            Assert.That(tagsWork.Album.Value, Is.EqualTo(tagsGood.Album.Value));
            Assert.That(tagsWork.Album.Value, Is.Not.EqualTo(badAlbum));
            Assert.That(tagsWork.Artists.Value, Is.EqualTo(tagsGood.Artists.Value));
            Assert.That(string.Join(' ', tagsWork.Artists.Value), Is.Not.EqualTo(badArtist));
            Assert.That(tagsWork.Title.Value, Is.EqualTo(tagsGood.Title.Value));
            Assert.That(tagsWork.Title.Value, Is.Not.EqualTo(badTitle));
        });

        mp3work.Dispose();
        mp3good.Dispose();
    }
}