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
    public partial class Card
    {
        #region Primitive Properties
    
        public virtual int idCard
        {
            get;
            set;
        }
    
        public virtual string devicePath
        {
            get;
            set;
        }
    
        public virtual string name
        {
            get;
            set;
        }
    
        public virtual int priority
        {
            get;
            set;
        }
    
        public virtual bool grabEPG
        {
            get;
            set;
        }
    
        public virtual System.DateTime lastEpgGrab
        {
            get;
            set;
        }
    
        public virtual string recordingFolder
        {
            get;
            set;
        }
    
        public virtual int idServer
        {
            get { return _idServer; }
            set
            {
                if (_idServer != value)
                {
                    if (Server != null && Server.idServer != value)
                    {
                        Server = null;
                    }
                    _idServer = value;
                }
            }
        }
        private int _idServer;
    
        public virtual bool enabled
        {
            get;
            set;
        }
    
        public virtual int camType
        {
            get;
            set;
        }
    
        public virtual string timeshiftingFolder
        {
            get;
            set;
        }
    
        public virtual int recordingFormat
        {
            get;
            set;
        }
    
        public virtual int decryptLimit
        {
            get;
            set;
        }
    
        public virtual bool preload
        {
            get;
            set;
        }
    
        public virtual bool CAM
        {
            get;
            set;
        }
    
        public virtual byte NetProvider
        {
            get;
            set;
        }
    
        public virtual bool stopgraph
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual ICollection<CardGroupMap> CardGroupMaps
        {
            get
            {
                if (_cardGroupMaps == null)
                {
                    var newCollection = new FixupCollection<CardGroupMap>();
                    newCollection.CollectionChanged += FixupCardGroupMaps;
                    _cardGroupMaps = newCollection;
                }
                return _cardGroupMaps;
            }
            set
            {
                if (!ReferenceEquals(_cardGroupMaps, value))
                {
                    var previousValue = _cardGroupMaps as FixupCollection<CardGroupMap>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupCardGroupMaps;
                    }
                    _cardGroupMaps = value;
                    var newValue = value as FixupCollection<CardGroupMap>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupCardGroupMaps;
                    }
                }
            }
        }
        private ICollection<CardGroupMap> _cardGroupMaps;
    
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
    
        public virtual ICollection<ChannelMap> ChannelMaps
        {
            get
            {
                if (_channelMaps == null)
                {
                    var newCollection = new FixupCollection<ChannelMap>();
                    newCollection.CollectionChanged += FixupChannelMaps;
                    _channelMaps = newCollection;
                }
                return _channelMaps;
            }
            set
            {
                if (!ReferenceEquals(_channelMaps, value))
                {
                    var previousValue = _channelMaps as FixupCollection<ChannelMap>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupChannelMaps;
                    }
                    _channelMaps = value;
                    var newValue = value as FixupCollection<ChannelMap>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupChannelMaps;
                    }
                }
            }
        }
        private ICollection<ChannelMap> _channelMaps;
    
        public virtual Server Server
        {
            get { return _server; }
            set
            {
                if (!ReferenceEquals(_server, value))
                {
                    var previousValue = _server;
                    _server = value;
                    FixupServer(previousValue);
                }
            }
        }
        private Server _server;

        #endregion
        #region Association Fixup
    
        private void FixupServer(Server previousValue)
        {
            if (previousValue != null && previousValue.Cards.Contains(this))
            {
                previousValue.Cards.Remove(this);
            }
    
            if (Server != null)
            {
                if (!Server.Cards.Contains(this))
                {
                    Server.Cards.Add(this);
                }
                if (idServer != Server.idServer)
                {
                    idServer = Server.idServer;
                }
            }
        }
    
        private void FixupCardGroupMaps(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (CardGroupMap item in e.NewItems)
                {
                    item.Card = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (CardGroupMap item in e.OldItems)
                {
                    if (ReferenceEquals(item.Card, this))
                    {
                        item.Card = null;
                    }
                }
            }
        }
    
        private void FixupDisEqcMotors(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (DisEqcMotor item in e.NewItems)
                {
                    item.Card = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (DisEqcMotor item in e.OldItems)
                {
                    if (ReferenceEquals(item.Card, this))
                    {
                        item.Card = null;
                    }
                }
            }
        }
    
        private void FixupChannelMaps(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (ChannelMap item in e.NewItems)
                {
                    item.Card = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (ChannelMap item in e.OldItems)
                {
                    if (ReferenceEquals(item.Card, this))
                    {
                        item.Card = null;
                    }
                }
            }
        }

        #endregion
    }
}
