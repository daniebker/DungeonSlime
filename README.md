## mgcb editor

### on mac

Follow setup instructions [here](https://docs.monogame.net/articles/getting_started/1_setting_up_your_os_for_development_macos.html?tabs=android)

```sh
echo export MGFXC_WINE_PATH=~/.winemonogame >> ~/.zshrc
source ~/.zshrc
```

See [github issue](https://github.com/MonoGame/MonoGame/issues/7423)

```sh
cd DungeonSlime
dotnet mgcb-editor-mac $(realpath ./Content/Content.mgcb)
```

## Release

```sh
cd DungeonSlime/ 
dotnet tool install MonoPack -g
monopack -p DungeonSlime.csproj
```

## References

- [MonoGame Building 2D Games Tutorial](https://docs.monogame.net/articles/tutorials/building_2d_games/index.html)
- [Unto Deeper Depths](https://www.youtube.com/@TheShaggyDev/videos)