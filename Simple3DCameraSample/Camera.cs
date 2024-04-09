using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Simple3DCameraSample
{
    internal class Camera
    {
        Vector3 position;
        Quaternion rotation;
        float aspectRatio;

        public Camera(Vector3 position, Quaternion rotation, float aspectRatio)
        {
            this.position = position;
            this.rotation = rotation;
            this.aspectRatio = aspectRatio;
        }

        public void Update()
        {
            //hold alt and move mouse to yaw or pitch the camera
            //note since we arent using a fixed up vector for CreateLookAt that the camera is free to roll around as well
            //if you want a fixed "Up" uncomment the line in ApplyTo, but note that rolling will no longer apply
            if (Input.isKeyDown(Keys.LeftAlt))
            {
                //1 mouse pixel movement = 1 degree
                this.rotation *= Quaternion.CreateFromYawPitchRoll(
                    MathHelper.ToRadians(Input.mouseMove.X),
                    MathHelper.ToRadians(Input.mouseMove.Y), 
                    //Q and E to roll (1 degree per Update)
                    MathHelper.ToRadians(
                        (Input.isKeyDown(Keys.Q) ? 1 : 0) +
                        (Input.isKeyDown(Keys.E) ? -1 : 0)
                    )
                    );
            }

            //WASD to move (Space and C to move up/down)
            if (Input.isKeyDown(Keys.W)) { position += Vector3.Transform(Vector3.Forward, rotation); }
            if (Input.isKeyDown(Keys.S)) { position += Vector3.Transform(Vector3.Backward, rotation); }
            if (Input.isKeyDown(Keys.A)) { position += Vector3.Transform(Vector3.Left, rotation); }
            if (Input.isKeyDown(Keys.D)) { position += Vector3.Transform(Vector3.Right, rotation); }
            if (Input.isKeyDown(Keys.Space)) { position += Vector3.Transform(Vector3.Up, rotation); }
            if (Input.isKeyDown(Keys.C)) { position += Vector3.Transform(Vector3.Down, rotation); }
        }

        public void ApplyTo(BasicEffect effect)
        {
            effect.World = Matrix.CreateWorld(Vector3.Zero, Vector3.Forward, Vector3.Up);
            //What is "Forward" depends on the rotation of the camera, so we need to Transform() the normal Forward vector based on the camera's rotation
            //What is "Up" also depend on this if we aren't using a fixed "Up"
            effect.View = Matrix.CreateLookAt(position, position + Vector3.Transform(Vector3.Forward, rotation), Vector3.Transform(Vector3.Up, rotation));
            //effect.View = Matrix.CreateLookAt(position, position + Vector3.Transform(Vector3.Forward, rotation), Vector3.Up);
            effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), aspectRatio, 1, 5000);
        }

    }
}
