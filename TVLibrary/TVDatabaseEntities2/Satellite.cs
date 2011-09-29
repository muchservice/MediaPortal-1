//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.Specialized;

namespace TVDatabaseEntities
{
    public partial class Satellite
    {
        #region Primitive Properties
    
        public virtual int idSatellite
        {
            get;
            set;
        }
    
        public virtual string satelliteName
        {
            get;
            set;
        }
    
        public virtual string transponderFileName
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual ICollection<DisEqcMotor> DisEqcMotors
        {
            get
            {
                if (_disEqcMotors == null)
                {
                    var newCollection = new FixupCollection<DisEqcMotor>();
                    newCollection.CollectionChanged += FixupDisEqcMotors;
                    _disEqcMotors = newCollection;
                }
                return _disEqcMotors;
            }
            set
            {
                if (!ReferenceEquals(_disEqcMotors, value))
                {
                    var previousValue = _disEqcMotors as FixupCollection<DisEqcMotor>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupDisEqcMotors;
                    }
                    _disEqcMotors = value;
                    var newValue = value as FixupCollection<DisEqcMotor>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupDisEqcMotors;
                    }
                }
            }
        }
        private ICollection<DisEqcMotor> _disEqcMotors;

        #endregion
        #region Association Fixup
    
        private void FixupDisEqcMotors(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (DisEqcMotor item in e.NewItems)
                {
                    item.Satellite = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (DisEqcMotor item in e.OldItems)
                {
                    if (ReferenceEquals(item.Satellite, this))
                    {
                        item.Satellite = null;
                    }
                }
            }
        }

        #endregion
    }
}
