using OpenTK;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;
using System.Threading.Tasks;
using System.Collections.Generic;

class Volume
{
    public short[] mData;
    public int mWidth = 0,
               mHeight = 0,
               mDepth = 0,
               mMin,
               mMax,
               mStep,
               countLayers = 0,
               mShaderProgramId,
               rotation;
    public bool mirror = false;


    int mTexIDSlice = -1;
    int mTexIDMask = -1;

    public int mFragShader;
    public int mVertexShader;

    float step;

    public int[,] maskManager = new int[10, 2];
    public Bitmap[] mBitmapList = new Bitmap[10];

    public float mXScale,
                 mYScale,
                 mZScale;
    public Bitmap mCurrentImage,
           img = new Bitmap(1, 1),
           layerImage = new Bitmap(1, 1);

    public Volume()
    {
        GL.ShadeModel(ShadingModel.Smooth);
        InitShaderProgram();
    }

    public int clamp(int value, int min, int max)
    {
        return Math.Min(max, Math.Max(min, value));
    }

    public void MirroringMap()
    {
        int length = mHeight * mWidth * mDepth;
        short[] iData = new short[length];
        for (int j = 0; j < mDepth; j++)
        {
            for (int i = 0; i < mHeight; i++)
            {
                for (int k = 0; k < mWidth; k++)
                {
                    //int _currentlayer = (mWidth) * (mHeight) * j;

                    iData[j * mWidth * mHeight + i * mHeight + k] = mData[j * mWidth * mHeight + i * mHeight + k];
                }
            }
        }
        mData = iData;
        iData = new short[0];
        return;
    }
    private Bitmap CreateLayerBitmap(int currentLayer, int maxDensity, int minDensity)
    {
        currentLayer = clamp(currentLayer, 0, mDepth);
        int z = mWidth * mHeight * currentLayer;
        step = 255.0f / (maxDensity - minDensity);
        layerImage.Dispose();
        layerImage = new Bitmap(mWidth, mHeight);
        Color color;
        for (int y = 0; y < mHeight; y++)
            for (int x = 0; x < mWidth; x++)
            {
                short currDensity = mData[x + y * mWidth + z];
                int colorValue = (int)((currDensity - mMin) * step);
                if (colorValue <= 0)
                    color = Color.White;
                else if (colorValue >= 255)
                    color = Color.Black;
                else
                    color = Color.FromArgb(colorValue, colorValue, colorValue);

                layerImage.SetPixel(x, y, color);
            }
        return layerImage;
    }

    void initShader(ref int shaderObj, string shaderSource, ShaderType type)
    {
        shaderObj = GL.CreateShader(type);
        GL.ShaderSource(shaderObj, File.ReadAllText(shaderSource));
        GL.CompileShader(shaderObj);
        // GL.GetShader(shaderObj, ShaderParameter.CompileStatus, out int statusCode);
    }
    private void InitShaderProgram()
    {
        initShader(ref mVertexShader, "../../shader.vert", ShaderType.VertexShader);
        initShader(ref mFragShader, "../../shader.frag", ShaderType.FragmentShader);

        mShaderProgramId = GL.CreateProgram();
        GL.AttachShader(mShaderProgramId, mVertexShader);
        GL.AttachShader(mShaderProgramId, mFragShader);
        GL.LinkProgram(mShaderProgramId);
        GL.BindFragDataLocation(mShaderProgramId, 0, "FragColor");
    }

    public void drawViaShaders()
    {
        GL.ClearColor(Color.White);
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        GL.Enable(EnableCap.Texture2D);

        GL.UseProgram(mShaderProgramId);

        GL.ActiveTexture(TextureUnit.Texture0 + mTexIDSlice);
        GL.BindTexture(TextureTarget.Texture2D, mTexIDSlice);
        GL.Uniform1(GL.GetUniformLocation(mShaderProgramId, "uTextureSlice"), mTexIDSlice);
        GL.ActiveTexture(TextureUnit.Texture0 + mTexIDMask);
        GL.BindTexture(TextureTarget.Texture2D, mTexIDMask);
        GL.Uniform1(GL.GetUniformLocation(mShaderProgramId, "uTextureMask"), mTexIDMask);

        drawQuad();

        GL.Disable(EnableCap.Texture2D);
    }

    /*public void drawTexturedSlice()
    {
        if (mCurrentImage == null)
            return;

        GL.ClearColor(Color.White);
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        GL.Enable(EnableCap.Texture2D);

        GL.Begin(PrimitiveType.Quads);

        GL.TexCoord2(0.0, 0.0);
        GL.Vertex2(-1.0f, -1.0f);

        GL.TexCoord2(0.0, 1.0);
        GL.Vertex2(-1.0f, 1.0f);

        GL.TexCoord2(1.0, 1.0);
        GL.Vertex2(1.0f, 1.0f);

        GL.TexCoord2(1.0, 0.0);
        GL.Vertex2(1.0f, -1.0f);

        GL.End();
        GL.Disable(EnableCap.Texture2D);
    }*/

    private void drawQuad()
    {
        GL.Begin(PrimitiveType.Quads);

        GL.TexCoord2(0.0, 0.0);
        GL.Vertex2(-1.0f, -1.0f);

        GL.TexCoord2(0.0, 1.0);
        GL.Vertex2(-1.0f, 1.0f);

        GL.TexCoord2(1.0, 1.0);
        GL.Vertex2(1.0f, 1.0f);

        GL.TexCoord2(1.0, 0.0);
        GL.Vertex2(1.0f, -1.0f);

        GL.End();
    }

    public int Load2DTexture(Bitmap textureImage)
    {
        int texID = GL.GenTexture();

        GL.BindTexture(TextureTarget.Texture2D, texID);
        BitmapData data = textureImage.LockBits(
            new System.Drawing.Rectangle(0, 0, textureImage.Width, textureImage.Height),
            ImageLockMode.ReadOnly,
            System.Drawing.Imaging.PixelFormat.Format32bppArgb);

        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba,
            data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
            PixelType.UnsignedByte, data.Scan0);

        textureImage.UnlockBits(data);

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
            (int)TextureMinFilter.Linear);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
            (int)TextureMagFilter.Linear);

        return texID;
    }

    public bool LoadData(string fileName)
    {
        if (File.Exists(fileName))
        {
            try
            {
                BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open));

                mWidth = reader.ReadInt32();
                mHeight = reader.ReadInt32();
                mDepth = reader.ReadInt32();

                mXScale = reader.ReadSingle();
                mYScale = reader.ReadSingle();
                mZScale = reader.ReadSingle();

                int length = mWidth * mHeight * mDepth;

                mData = new short[length];

                for (int i = 0; i < length; i++)
                {
                    mData[i] = reader.ReadInt16();
                }

                mMin = mData.Min();
                mMax = mData.Max();

                mStep = mMax / 255;

                //MirroringMap();
                mCurrentImage = new Bitmap(mWidth, mHeight);

                reader.Close();


                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        else
        {
            MessageBox.Show("There is no file with name" + fileName);
            return false;
        }
    }

    public double getMaskMin(int currentlayer, Bitmap mask, Color col)
    {
        currentlayer = clamp(currentlayer, 0, mDepth);
        short min = mData[1];

        int z = mWidth * mHeight * currentlayer,
            n = 0;

        for (int x = 0; x < mask.Width; x++)
            for (int y = 0; y < mask.Height; y++)
            {
                if (mask.GetPixel(x, y).ToArgb() == col.ToArgb())
                {
                    n++;
                    short currDensity = mData[x + y * mWidth + z];
                    if (currDensity < min)
                        min = currDensity;
                }
            }
        return (double)min;
    }

    public double getMaskMax(int currentlayer, Bitmap mask, Color col)
    {
        currentlayer = clamp(currentlayer, 0, mDepth);
        short max = mData[1];

        int z = mWidth * mHeight * currentlayer,
            n = 0;

        for (int x = 0; x < mask.Width; x++)
            for (int y = 0; y < mask.Height; y++)
            {
                if (mask.GetPixel(x, y).ToArgb() == col.ToArgb())
                {
                    n++;
                    short currDensity = mData[x + y * mWidth + z];
                    if (currDensity > max)
                        max = currDensity;
                }
            }
        return (double)max;
    }

    public double getMaskAVG(int currentlayer, Bitmap mask, Color col)
    {
        currentlayer = clamp(currentlayer, 0, mDepth);
        float a = 0, avg;

        int z = mWidth * mHeight * currentlayer,
            n = 0;

        for (int x = 0; x < mask.Width; x++)
            for (int y = 0; y < mask.Height; y++)
            {
                if (mask.GetPixel(x, y).ToArgb() == col.ToArgb())
                {
                    n++;
                    short currDensity = mData[x + y * mWidth + z];
                    a = (float)(a + currDensity);
                }
            }
        avg = (float)(a / n);
        return avg;
    }

    public double getMaskStDeviation(int currentlayer, Bitmap mask, Color col)
    {
        currentlayer = clamp(currentlayer, 0, mDepth);
        float a = 0, σ = 0, avg;

        int z = mWidth * mHeight * currentlayer,
            n = 0;

        avg = (float)(a / n);
        for (int x = 0; x < mask.Width; x++)
            for (int y = 0; y < mask.Height; y++)
            {
                if (mask.GetPixel(x, y).ToArgb() == col.ToArgb())
                {
                    n++;
                    short currDensity = mData[x + y * mWidth + z];
                    a = (float)(a + currDensity);
                }
            }
        avg = (float)(a / n);
        for (int x = 0; x < mask.Width; x++)
            for (int y = 0; y < mask.Height; y++)
            {
                if (mask.GetPixel(x, y).ToArgb() == col.ToArgb())
                {
                    short currDensity = mData[x + y * mWidth + z];
                    σ = (float)(σ + Math.Pow((currDensity - avg), 2));
                }
            }
        σ = (float)Math.Sqrt(σ / n);

        return (double)σ;
    }

    public void GetMaskinfo(int currentlayer, Bitmap mask, Color col)
    {
        currentlayer = clamp(currentlayer, 0, mDepth);
        short min = mData[1], max = mData[1];
        float a = 0, σ = 0, avg;

        int z = mWidth * mHeight * currentlayer,
            n = 0;

        for (int x = 0; x < mask.Width; x++)
            for (int y = 0; y < mask.Height; y++)
            {
                if (mask.GetPixel(x, y).ToArgb() == col.ToArgb())
                {
                    n++;
                    short currDensity = mData[x + y * mWidth + z];
                    a = (float)(a + currDensity);
                    if (currDensity > max)
                        max = currDensity;
                    if (currDensity < min)
                        min = currDensity;
                }
            }
        avg = (float)(a / n);
        for (int x = 0; x < mask.Width; x++)
            for (int y = 0; y < mask.Height; y++)
            {
                if (mask.GetPixel(x, y).ToArgb() == col.ToArgb())
                {
                    short currDensity = mData[x + y * mWidth + z];
                    σ = (float)(σ + Math.Pow((currDensity - avg), 2));
                }
            }
        σ = (float)Math.Sqrt(σ / n);

        MessageBox.Show("Min: " + min.ToString() + "\nMax: " + max.ToString() + "\nAVG: " + (avg).ToString() + "\nσ: " + σ.ToString(), "Min/Max/AVG/σ", MessageBoxButtons.OK);
    }

    public int GetVol(int volMin, int volMax, string side)
    {
        int amount = 0;
        for (int i = 0; i < mHeight * mWidth; i++)
        {
            if (mData[i] >= volMin)
                if (mData[i] <= volMax)
                    amount++;
        }
        return amount;
    }

    public int GetVolFm(Bitmap bitc, Color col)
    {
        Bitmap bit = new Bitmap(bitc);
        int amount = 0;
        for (int x = 0; x < bit.Width; x++)
            for (int y = 0; y < bit.Height; y++)
            {
                if (bit.GetPixel(x, y).ToArgb() == col.ToArgb())
                    amount++;
            }
        bit.Dispose();
        return amount;
    }

    public void ChangeMask(int currentmask)
    {
        if (currentmask >= 0)
            img = mBitmapList[currentmask];
    }

    public void LoadTexture(int currentlayer, int maxDensity, int minDensity)
    {
        if (mWidth * mHeight * mDepth == 0)
            return;

        mCurrentImage = CreateLayerBitmap(currentlayer, maxDensity, minDensity);

        GL.DeleteTexture(mTexIDSlice);
        GL.DeleteTexture(mTexIDMask);
        mTexIDSlice = Load2DTexture(mCurrentImage);
        mCurrentImage.Dispose();
        mTexIDMask = Load2DTexture(img);
    }
}
