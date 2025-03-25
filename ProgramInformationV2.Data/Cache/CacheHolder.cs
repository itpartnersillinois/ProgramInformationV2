namespace ProgramInformationV2.Data.Cache {

    public class CacheHolder {
        private readonly Dictionary<string, CacheThinObject> _dictionary = [];

        public bool ClearCache(string netid) => _dictionary.Remove(netid);

        public string? GetCacheSource(string netid) => _dictionary.ContainsKey(netid) ? _dictionary[netid].Source : null;

        public CacheThinObject? GetItem(string netid) => _dictionary.ContainsKey(netid) ? _dictionary[netid] : null;

        public bool HasCachedItem(string netid) => _dictionary.ContainsKey(netid);

        public void SetCache(CacheThinObject cacheThinObject) {
            if (_dictionary.ContainsKey(cacheThinObject.NetId)) {
                _dictionary[cacheThinObject.NetId] = cacheThinObject;
            } else {
                _dictionary.Add(cacheThinObject.NetId, cacheThinObject);
            }
            ClearExpired();
        }

        public void SetCacheItem(string netid, string id) {
            if (_dictionary.ContainsKey(netid)) {
                _dictionary[netid].ItemId = id;
                _dictionary[netid].Reset();
            }
        }

        public void SetCacheParentItem(string netid, string parentId) {
            if (_dictionary.ContainsKey(netid)) {
                _dictionary[netid].ParentId = parentId;
                _dictionary[netid].ItemId = "";
                _dictionary[netid].Reset();
            }
        }

        public void SetCacheSource(string netid, string source) {
            if (_dictionary.ContainsKey(netid)) {
                _dictionary[netid].Source = source;
                _dictionary[netid].Reset();
            } else {
                _dictionary.Add(netid, new CacheThinObject(netid) { Source = source });
            }
            ClearExpired();
        }

        private void ClearExpired() {
            foreach (var items in _dictionary.Where(i => i.Value != null && i.Value.Expired)) {
                _dictionary.Remove(items.Key);
            }
        }
    }
}