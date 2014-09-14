using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Drawing;

namespace Cap10Slog.View.LogRecordIcon
{
    public class LogRecordIconFactory
    {
        private static Queue<ConstructorInfo> constructors = new Queue<ConstructorInfo>();

        private static Queue<Color> colors = new Queue<Color>();
        private static Queue<Color> usedColors = new Queue<Color>();

        private static Type[] construcorArgumentList = new Type[] { typeof(Color) };

        static LogRecordIconFactory()
        {
            colors.Enqueue(Color.Red);
            colors.Enqueue(Color.Green);
            colors.Enqueue(Color.Blue);
            colors.Enqueue(Color.Purple);
            colors.Enqueue(Color.Brown);
            colors.Enqueue(Color.Yellow);
            colors.Enqueue(Color.Orange);
            colors.Enqueue(Color.Gray);

            constructors.Enqueue(typeof(Circle).GetConstructor(construcorArgumentList));
            constructors.Enqueue(typeof(Square).GetConstructor(construcorArgumentList));
            constructors.Enqueue(typeof(Diamond).GetConstructor(construcorArgumentList));
            constructors.Enqueue(typeof(Triangle1).GetConstructor(construcorArgumentList));
            constructors.Enqueue(typeof(Triangle2).GetConstructor(construcorArgumentList));
            constructors.Enqueue(typeof(EmptyCircle).GetConstructor(construcorArgumentList));
            constructors.Enqueue(typeof(EmptySquare).GetConstructor(construcorArgumentList));
            constructors.Enqueue(typeof(EmptyDiamond).GetConstructor(construcorArgumentList));
            constructors.Enqueue(typeof(EmptyTriangle1).GetConstructor(construcorArgumentList));
            constructors.Enqueue(typeof(EmptyTriangle2).GetConstructor(construcorArgumentList));
            constructors.Enqueue(typeof(X).GetConstructor(construcorArgumentList));
            constructors.Enqueue(typeof(Plus).GetConstructor(construcorArgumentList));



        }

        public static ILogRecordIcon GetNext()
        {
            ConstructorInfo ci = constructors.Peek();

            Color color = colors.Dequeue();
            usedColors.Enqueue(color);

            ILogRecordIcon result = (ILogRecordIcon)ci.Invoke(new object[] { color });

            if (colors.Count == 0)
            {
                Queue<Color> temp = usedColors;
                
                usedColors = colors;
                colors = temp;

                ci = constructors.Dequeue();
                constructors.Enqueue(ci);
            }

            return result;
        }
    }
}
