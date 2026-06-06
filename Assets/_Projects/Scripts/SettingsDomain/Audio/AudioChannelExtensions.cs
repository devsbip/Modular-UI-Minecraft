public static class AudioChannelExtensions
{
    public static string GetUIName(this AudioChannel channel)
    {
        return channel switch
        {
            AudioChannel.Jukebox => "Jukebox/Note Blocks",
            AudioChannel.Hostile => "Hostile Creatures",
            AudioChannel.Friendly => "Friendly Creatures",
            AudioChannel.Ambient => "Ambient/Environment",
            AudioChannel.Narrator => "Narrator/Speech",
            _ => channel.ToString()
        };
    }
}