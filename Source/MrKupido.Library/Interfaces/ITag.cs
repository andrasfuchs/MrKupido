using MrKupido.Library.Ingredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library
{
    public interface ITag
    {
		float Match(ITreeNode r);

		bool IsMatch(ITreeNode r);
    }
}
