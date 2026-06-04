public static class AudioChannelExtensions
{
    public static string GetUIName(this AudioChannel channel)
    {
        switch (channel)
        {
            case AudioChannel.Jukebox: 
                return "Jukebox/Note Blocks"; 
            case AudioChannel.Hostile: 
                return "Hostile Creatures"; 
            case AudioChannel.Friendly: 
                return "Friendly Creatures"; 
            case AudioChannel.Ambient:
                return "Ambient/Environment";
            case AudioChannel.Narrator: 
                return "Narrator/Speech"; 
            
            default:
                return channel.ToString();
        }
    
    }
}