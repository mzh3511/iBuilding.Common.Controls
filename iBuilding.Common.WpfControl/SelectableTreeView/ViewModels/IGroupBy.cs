using System.Collections.Generic;

namespace RanOpt.iBuilding.Common.WpfControl.SelectableTreeView.ViewModels
{
    public interface IGroupBy<TItem>
    {
        void Execute(TreeNodeViewModel root, IList<TItem> items);
    }
}