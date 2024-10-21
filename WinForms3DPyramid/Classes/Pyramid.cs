using System;
using System.Collections.Generic;
using System.Drawing;
namespace WinForms3DPyramid
{
    public class Pyramid
    {
        //Поле, хранящее массив точек вершин пирамиды
        private readonly float[][] points3D;
        
        public Pyramid(float scale = 1f)
        {
            float[][] basePoints3D = new float[5][] {
                //Общая вершина пирамиды
                new float[] { 0, 150, 100 },
                //Вершины основания пирамиды
                new float[] { -100, -100, 100 },
                new float[] { 100, -100, 100 },
                new float[] { 100, -100, -100 },
                new float[] { -100, -100, -100 },
            };
            points3D = new float[5][];
            //Скалирования размера пирамиды
            for(int i =  0; i < basePoints3D.Length; i++)
            {
                points3D[i] = new float[3];
                for(int j = 0;j < basePoints3D[i].Length;j++)
                {
                    points3D[i][j] = basePoints3D[i][j] * scale;
                }
            }
        }
        public float[][] GetPoints()
        {
            return points3D;
        }
    }
}
