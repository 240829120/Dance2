﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 列表
    /// </summary>
    /// <param name="manager">历史管理器</param>
    /// <typeparam name="T">子元素类型</typeparam>
    public class DanceObservableCollection<T> : ObservableCollection<T>
    {
        /// <summary>
        /// 类别
        /// </summary>
        public DanceObservableCollection() { }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="collection">数据项</param>
        public DanceObservableCollection(IEnumerable<T> collection) : base(collection) { }

        /// <summary>
        /// <inheritdoc cref="IDanceHistoryManager"/>
        /// </summary>
        public IDanceHistoryManager? HistoryManager { get; set; }

        /// <summary>
        /// 列表改变
        /// </summary>
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (this.HistoryManager != null && !this.HistoryManager.IsExecuting)
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        this.HistoryManager.Append(new DanceCollectionAddHistoryStep(this, e.NewStartingIndex, e.NewItems ?? new List<T>()));
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        this.HistoryManager.Append(new DanceCollectionRemoveHistoryStep(this, e.OldStartingIndex, e.OldItems ?? new List<T>()));
                        break;
                    case NotifyCollectionChangedAction.Replace:
                        this.HistoryManager.Append(new DanceCollectionReplaceHistoryStep(this, e.OldStartingIndex, e.OldItems ?? new List<T>(), e.NewStartingIndex, e.NewItems ?? new List<T>()));
                        break;
                    case NotifyCollectionChangedAction.Move:
                        this.HistoryManager.Append(new DanceCollectionMoveHistoryStep(this, e.OldStartingIndex, e.NewStartingIndex));
                        break;
                    case NotifyCollectionChangedAction.Reset:
                        this.HistoryManager.Append(new DanceCollectionResetHistoryStep(this, this.ToList()));
                        break;
                }
            }

            if (DanceModelBase.DispatcherCheckAccess == null || DanceModelBase.DispatcherCheckAccess())
            {
                base.OnCollectionChanged(e);
            }
            else
            {
                DanceModelBase.DispatcherInvoke?.Invoke(() =>
                {
                    base.OnCollectionChanged(e);
                });
            }
        }
    }
}
