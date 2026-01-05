using BattleCompanies.Scenes;
using Microsoft.Xna.Framework.Media;
using BattleCompanies.AppLib;

namespace BattleCompanies;

public class GameMain : Core
{
    // The background theme song.
    private Song _themeSong;

    public GameMain() : base("Battle Companies", 1280, 720, false)
    {

    }

    protected override void Initialize()
    {
        base.Initialize();

        // Start playing the background music.
        Audio.PlaySong(_themeSong);

        // Start the game with the title scene.
        ChangeScene(new TitleScene());
    }

    protected override void LoadContent()
    {
        // Load the background theme music.
        _themeSong = Content.Load<Song>("audio/theme");
    }
}
