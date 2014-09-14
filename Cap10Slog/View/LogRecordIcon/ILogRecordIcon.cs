using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace Cap10Slog.View.LogRecordIcon
{
    public interface ILogRecordIcon
    {
        void Draw(Graphics g, Rectangle r);
    }
}