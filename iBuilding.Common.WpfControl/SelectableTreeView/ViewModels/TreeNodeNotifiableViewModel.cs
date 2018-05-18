using System;
using System.Collections.Generic;
using System.Linq;

namespace RanOpt.iBuilding.Common.WpfControl.SelectableTreeView.ViewModels
{
    /// <summary>
    /// The view model for tree node, which can receive sub nodes's property change notification and report parent node that
    /// its property changed.
    /// </summary>
    public class TreeNodeNotifiableViewModel : TreeNodeBaseViewModel
    {
        private Action<TreeNodeNotifiableViewModel, string> _propertyChangedNotification;

        public new IEnumerable<TreeNodeNotifiableViewModel> Nodes =>
            base.Nodes.OfType<TreeNodeNotifiableViewModel>();

        public void AddNode(TreeNodeNotifiableViewModel node)
        {
            node._propertyChangedNotification = ReceiveSubPropertyChanged;
            base.AddNode(node);
        }

        /// <summary>
        /// Receive its one sub node's one property has changed
        /// </summary>
        /// <param name="obj"> The sub node which property changed </param>
        /// <param name="propertyName"> The changed property name </param>
        protected void ReceiveSubPropertyChanged(TreeNodeNotifiableViewModel obj, string propertyName)
        {
            if (!OnSubPropertyChanged(propertyName))
                return;
            OnPropertyChanged(propertyName);
            NotifyParentPropertyChanged(obj, propertyName);
        }

        /// <summary>
        /// The method will be called when receive sub items property change notification.
        /// </summary>
        /// <param name="propertyName"> The changed property name </param>
        /// <returns>
        /// if current item property also changed because of the notification in the method, it returns <code>true</code>
        /// </returns>
        protected virtual bool OnSubPropertyChanged(string propertyName)
        {
            return false;
        }

        /// <summary>
        /// Report to its parent that its one property has changed.
        /// </summary>
        /// <param name="obj"> The object which property value changed </param>
        /// <param name="propertyName"> The changed property name </param>
        protected void NotifyParentPropertyChanged(TreeNodeNotifiableViewModel obj, string propertyName)
        {
            _propertyChangedNotification?.Invoke(obj, propertyName);
        }
    }
}