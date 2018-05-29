using System.Drawing;

class Control
{
    public int[,] maskManager = new int[10, 2];
    public int countLayers = 0;
    public Bitmap[] mBitmapList = new Bitmap[10];
    public Volume lVolume, rVolume;

    public void ChangeMask(int currentmask)
    {
        if (currentmask >= 0)
        {
            lVolume.img = mBitmapList[currentmask];
            rVolume.img = mBitmapList[currentmask];
        }
            
    }
}