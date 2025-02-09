namespace AniMate_app.Interfaces
{
    public interface IFullScreenService
    {
        void EnterFullScreen();
        void ExitFullScreen();
        void RestoreOriginal();
    }
}
