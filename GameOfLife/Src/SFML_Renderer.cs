using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    class SFML_Renderer
    {
        public RenderWindow Window { get; private set; }
        public uint Width { get; private set; }
        public uint Height { get; private set; }
        public float QuadWidth { get; set; }
        public float QuadHeight { get; set; }

        VertexArray vertexArray;
        public SFML_Renderer(RenderWindow window, uint resolutionWidth, uint resolutionHeight)
        {
            Window = window;
            Width = resolutionWidth;
            Height = resolutionHeight;
            QuadHeight = 32;
            QuadWidth = 32;
            vertexArray = new VertexArray(PrimitiveType.Quads, resolutionWidth*resolutionHeight);
        }

        public void DrawBoard(byte[,] boardElements)
        {
            vertexArray.Clear();
            const int NUM_ELEMS_PER_PIXEL = 4;
            var size = boardElements.GetLength(0) * boardElements.GetLength(1) * NUM_ELEMS_PER_PIXEL;
            byte[] pixels = new byte[size];

            int count = 0;
            for (int y= 0; y < boardElements.GetLength(0); y++)
            {
                for(int x = 0; x < boardElements.GetLength(1); x++)
                {
                    float xPos = x * QuadWidth;
                    float yPos = y * QuadHeight;

                    if(boardElements[y,x] == 1)
                    {
                        InsertQuad(xPos,yPos,Color.White);
                    }             
                }
            }
            Window.Draw(vertexArray);
        }

        public void InsertQuad(float xPos, float yPos, Color color)
        {
            // 1 2
            // |_|
            // 0 3
            vertexArray.Append(new Vertex(new Vector2f(xPos, yPos), color));
            vertexArray.Append(new Vertex(new Vector2f(xPos, yPos + QuadHeight), color));
            vertexArray.Append(new Vertex(new Vector2f(xPos + QuadWidth, yPos + QuadHeight), color));
            vertexArray.Append(new Vertex(new Vector2f(xPos + QuadWidth, yPos), color));
        }
    }
}
