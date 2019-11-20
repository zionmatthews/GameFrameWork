using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GameFrameWork
{
    class Program
    {
        static void Main(string[] args)
        {
            //VectorTest();
            //Examples();
            //MatrixTest();
            //return;
             
            Game game = new Game();
            game.Run();
        }

        static void VectorTest()
        {

        }

        static void AABBTest()
        {

        }

        static void Examples()
        {
            /* Console.WriteLine(new Vector3(1f, 1f, 1f).Magnitude());
             Console.WriteLine(new Vector2(3f, -2f).Magnitude());
            Console.WriteLine(new Vector3(-1f, 1f, -1f).Magnitude());
             Console.WriteLine(new Vector3(0.5f, - 1f, 0.25f).Magnitude());

            Console.WriteLine(new Vector2(1f, 2f));
             Console.WriteLine(new Vector2(1f, 0f).Dot(new Vector2(0f, 1f)));
             Console.WriteLine(new Vector2(1f, 1f).Dot(new Vector2(-1f, -1f)));


             Console.WriteLine(new Vector3(2f, 3f, 1f).Cross(new Vector3(-3f, 1f, 2f)));

             Console.WriteLine(new Vector2(2f, 3f).GetAngle(new Vector2(0.5f, 1f)));
             Console.WriteLine(new Vector3(-0.5f, 0f, 2f).GetAngle(new Vector3(-3f, 1f, -1f)));

            Vector3 up = new Vector3(0f, 1f, 0f);
            Vector3 playerLoc = new Vector3(0, 0, 0);
            Vector3 enemyLoc = new Vector3(-7.5f, 0f, 9f);
            Vector3 enemyForward = new Vector3(0.857f, 0f, -0.514f);

            Vector3 enemyToPlayer = playerLoc - enemyLoc;

            Console.WriteLine("Distance from enemy to player: " + enemyToPlayer);




            if (enemyForward.Dot(enemyToPlayer) > 0)
            {
                Console.WriteLine("Player is in front of enemy.");
            }
            else
            {
                Console.WriteLine("Player is behind enemy. ");
            }

            Vector3 enemyLeft = enemyForward.Cross(up);

            if (enemyLeft.Dot(enemyToPlayer) > 0)
            {
                Console.WriteLine("Player is to the left of enemy.");
            }
            else
            {
                Console.WriteLine("Player is to the right  of enemy. ");
            }

            //Is the player in the enemy's FOV?
            if (enemyForward.GetAngle(enemyToPlayer) <= Math.PI / 4 || enemyForward.GetAngle(enemyToPlayer) >= 7 * Math.PI / 4)
            {
                Console.WriteLine("I' VE GOT YOU IN MY SIGHTS");
            }

            Console.ReadKey();*/
        }

        static void MatrixTest()
        {
            Matrix3 a = new Matrix3(1, 4, 7, 2, 5, 8, 3, 6, 9);
            Matrix3 b = new Matrix3(9, 6, 3, 8, 5, 2, 7, 4, 1);
            Matrix3 c = a * b;
            Console.WriteLine(c);

            Console.WriteLine(c * new Vector3(2, 4, 6));

            Console.WriteLine(a * new Matrix3());

            Console.ReadKey();
        }
    }
}
