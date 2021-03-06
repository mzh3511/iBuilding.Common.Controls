using System;
using System.Collections.Generic;
using System.Linq;

namespace RanOpt.iBuilding.Common.WpfControl.SelectableTreeView.ViewModels
{
    public class GroupBy<TItem, TParentGroupKey, TGroupKey> : AbstractGroupBy<TItem, TGroupKey>
    {
        public GroupBy(
            Func<TItem, TParentGroupKey, TGroupKey> keySelector,
            Func<TGroupKey, string> textSelector,
            Comparison<TGroupKey> comparer = null) : base(textSelector, comparer)
        {
            KeySelector = keySelector;
        }

        public Func<TItem, TParentGroupKey, TGroupKey> KeySelector { get; }

        protected override IEnumerable<IGrouping<TGroupKey, TItem>> Group(TreeNodeViewModel root,
            IList<TItem> items)
        {
            return items.GroupBy(item => KeySelector(item, (TParentGroupKey) root.NodeObj));
        }
    }
}