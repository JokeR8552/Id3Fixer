using Id3;

namespace Id3Fixer.Test
{
    [TestFixture]
    public class MainProgramTests
    {
        [Test]
        public void Positive()
        {
            var basePath = Path.Combine(Environment.CurrentDirectory, "Files");
            var playlistFileName = "play.aimppl4";
            File.Replace(Path.Combine(basePath, "02-02-Дружок_bad_backup.mp3"), Path.Combine(basePath, "02-02-Дружок_work.mp3"), null);

            var mp3bad = new Mp3(Path.Combine(basePath, "02-02-Дружок_work.mp3"), Mp3Permissions.Read);
            var tagsBad = mp3bad.GetTag(Id3TagFamily.Version2X);
            var badArtist = string.Join(' ', tagsBad.Artists.Value);
            var badAlbum = tagsBad.Album.Value;
            var badTitle = tagsBad.Title.Value;
            mp3bad.Dispose();

            Assert.DoesNotThrow(() => FixerProgram.Main(new string[] { basePath, playlistFileName }));

            using var mp3work = new Mp3(Path.Combine(basePath, "02-02-Дружок_work.mp3"), Mp3Permissions.Read);
            using var mp3good = new Mp3(Path.Combine(basePath, "02-02-Дружок_good.mp3"), Mp3Permissions.Read);
            var tagsWork = mp3work.GetTag(Id3TagFamily.Version2X);
            var tagsGood = mp3good.GetTag(Id3TagFamily.Version2X);
            Assert.Multiple(() =>
            {
                Assert.That(tagsWork.Album.Value, Is.EqualTo(tagsGood.Album.Value));
                Assert.That(tagsWork.Album.Value, Is.Not.EqualTo(badAlbum));
                Assert.That(tagsWork.Artists.Value, Is.EqualTo(tagsGood.Artists.Value));
                Assert.That(string.Join(' ', tagsWork.Artists.Value), Is.Not.EqualTo(badArtist));
                Assert.That(tagsWork.Title.Value, Is.EqualTo(tagsGood.Title.Value));
                Assert.That(tagsWork.Title.Value, Is.Not.EqualTo(badTitle));
            });
        }
    }
}