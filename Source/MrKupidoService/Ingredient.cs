//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;

namespace MrKupidoServices
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(Nutrition))]
    public partial class Ingredient: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public int IngredientId
        {
            get { return _ingredientId; }
            set
            {
                if (_ingredientId != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'IngredientId' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _ingredientId = value;
                    OnPropertyChanged("IngredientId");
                }
            }
        }
        private int _ingredientId;
    
        [DataMember]
        public string NameEng
        {
            get { return _nameEng; }
            set
            {
                if (_nameEng != value)
                {
                    _nameEng = value;
                    OnPropertyChanged("NameEng");
                }
            }
        }
        private string _nameEng;
    
        [DataMember]
        public string NameHun
        {
            get { return _nameHun; }
            set
            {
                if (_nameHun != value)
                {
                    _nameHun = value;
                    OnPropertyChanged("NameHun");
                }
            }
        }
        private string _nameHun;
    
        [DataMember]
        public int Category
        {
            get { return _category; }
            set
            {
                if (_category != value)
                {
                    _category = value;
                    OnPropertyChanged("Category");
                }
            }
        }
        private int _category;
    
        [DataMember]
        public string Classname
        {
            get { return _classname; }
            set
            {
                if (_classname != value)
                {
                    _classname = value;
                    OnPropertyChanged("Classname");
                }
            }
        }
        private string _classname;
    
        [DataMember]
        public Nullable<int> Index
        {
            get { return _index; }
            set
            {
                if (_index != value)
                {
                    _index = value;
                    OnPropertyChanged("Index");
                }
            }
        }
        private Nullable<int> _index;
    
        [DataMember]
        public Nullable<System.TimeSpan> ExpirationTime
        {
            get { return _expirationTime; }
            set
            {
                if (_expirationTime != value)
                {
                    _expirationTime = value;
                    OnPropertyChanged("ExpirationTime");
                }
            }
        }
        private Nullable<System.TimeSpan> _expirationTime;
    
        [DataMember]
        public Nullable<int> StorageTemperature
        {
            get { return _storageTemperature; }
            set
            {
                if (_storageTemperature != value)
                {
                    _storageTemperature = value;
                    OnPropertyChanged("StorageTemperature");
                }
            }
        }
        private Nullable<int> _storageTemperature;

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public TrackableCollection<Nutrition> Nutritions
        {
            get
            {
                if (_nutritions == null)
                {
                    _nutritions = new TrackableCollection<Nutrition>();
                    _nutritions.CollectionChanged += FixupNutritions;
                }
                return _nutritions;
            }
            set
            {
                if (!ReferenceEquals(_nutritions, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_nutritions != null)
                    {
                        _nutritions.CollectionChanged -= FixupNutritions;
                    }
                    _nutritions = value;
                    if (_nutritions != null)
                    {
                        _nutritions.CollectionChanged += FixupNutritions;
                    }
                    OnNavigationPropertyChanged("Nutritions");
                }
            }
        }
        private TrackableCollection<Nutrition> _nutritions;

        #endregion
        #region ChangeTracking
    
        protected virtual void OnPropertyChanged(String propertyName)
        {
            if (ChangeTracker.State != ObjectState.Added && ChangeTracker.State != ObjectState.Deleted)
            {
                ChangeTracker.State = ObjectState.Modified;
            }
            if (_propertyChanged != null)
            {
                _propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    
        protected virtual void OnNavigationPropertyChanged(String propertyName)
        {
            if (_propertyChanged != null)
            {
                _propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged{ add { _propertyChanged += value; } remove { _propertyChanged -= value; } }
        private event PropertyChangedEventHandler _propertyChanged;
        private ObjectChangeTracker _changeTracker;
    
        [DataMember]
        public ObjectChangeTracker ChangeTracker
        {
            get
            {
                if (_changeTracker == null)
                {
                    _changeTracker = new ObjectChangeTracker();
                    _changeTracker.ObjectStateChanging += HandleObjectStateChanging;
                }
                return _changeTracker;
            }
            set
            {
                if(_changeTracker != null)
                {
                    _changeTracker.ObjectStateChanging -= HandleObjectStateChanging;
                }
                _changeTracker = value;
                if(_changeTracker != null)
                {
                    _changeTracker.ObjectStateChanging += HandleObjectStateChanging;
                }
            }
        }
    
        private void HandleObjectStateChanging(object sender, ObjectStateChangingEventArgs e)
        {
            if (e.NewState == ObjectState.Deleted)
            {
                ClearNavigationProperties();
            }
        }
    
        protected bool IsDeserializing { get; private set; }
    
        [OnDeserializing]
        public void OnDeserializingMethod(StreamingContext context)
        {
            IsDeserializing = true;
        }
    
        [OnDeserialized]
        public void OnDeserializedMethod(StreamingContext context)
        {
            IsDeserializing = false;
            ChangeTracker.ChangeTrackingEnabled = true;
        }
    
        protected virtual void ClearNavigationProperties()
        {
            Nutritions.Clear();
        }

        #endregion
        #region Association Fixup
    
        private void FixupNutritions(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (Nutrition item in e.NewItems)
                {
                    item.Ingredient = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("Nutritions", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Nutrition item in e.OldItems)
                {
                    if (ReferenceEquals(item.Ingredient, this))
                    {
                        item.Ingredient = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("Nutritions", item);
                    }
                }
            }
        }

        #endregion
    }
}
