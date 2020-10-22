using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public ComputeShader computeShader;
    public RawImage rawImage;
    public Texture realTexture;

    private RenderTexture _renderTexture;

    public int point_size;
    public float coef;
    public int[] point_x;
    public int[] point_y;
    public float[] point_R;
    public float[] point_K;
    public Vector4[] point_C;
    public float[] point_THETA;
    public int[] point_x2;
    public int[] point_y2;

    void Start()
    {
        _renderTexture = new RenderTexture(1920, 1080, 24);
        _renderTexture.enableRandomWrite = true;
        _renderTexture.Create();

        rawImage.texture = _renderTexture;

        var kernel = computeShader.FindKernel("CSMain");
        Debug.Log("Kernel is " + kernel);
        computeShader.SetTexture(kernel, "result", _renderTexture);
        computeShader.SetTexture(kernel, "real", realTexture);
        //computeShader.Dispatch(_kernel, _renderTexture.width / 8, _renderTexture.height / 8, 1);
    }

    // Update is called once per frame
    void Update()
    {
        var  kernel = computeShader.FindKernel("CSMain");
        computeShader.SetInt( "point_size" , point_size );
        computeShader.SetFloat( "coef" , coef);
        computeShader.SetInts( "point_x" , point_x);
        computeShader.SetInts( "point_y" , point_y);
        computeShader.SetFloats( "point_R" , point_R);
        computeShader.SetFloats( "point_K" , point_K);
        computeShader.SetVectorArray( "point_C" , point_C);
        computeShader.SetFloats( "point_THETA" , point_THETA);
        computeShader.SetInts( "point_x2" , point_x2);
        computeShader.SetInts( "point_y2" , point_y2);
        computeShader.Dispatch( kernel , 1920 / 8, 1080 / 8, 1 );
    }
}