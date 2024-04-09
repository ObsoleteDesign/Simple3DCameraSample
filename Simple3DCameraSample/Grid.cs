using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Simple3DCameraSample
{
    internal class Grid
    {
        VertexBuffer vb;

        public Grid(GraphicsDevice gd)
        {
            List<VertexPosition> gridVertices = new List<VertexPosition>();
            for (var i = -100; i <= 100; i += 10)
            {
                gridVertices.Add(new VertexPosition(new Vector3(i, 0, -100)));
                gridVertices.Add(new VertexPosition(new Vector3(i, 0, 100)));
                for (var j = -100; j <= 100; j += 10)
                {
                    gridVertices.Add(new VertexPosition(new Vector3(-100, 0, j)));
                    gridVertices.Add(new VertexPosition(new Vector3(100, 0, j)));
                }
            }
            vb = new VertexBuffer(gd, typeof(VertexPosition), gridVertices.Count, BufferUsage.WriteOnly);
            vb.SetData(gridVertices.ToArray());
        }

        public void Draw(GraphicsDevice gd)
        {
            gd.SetVertexBuffer(vb);
            gd.DrawPrimitives(PrimitiveType.LineList, 0, vb.VertexCount / 2);
        }
    }
}
