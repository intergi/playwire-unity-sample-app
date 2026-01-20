using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

using System.Linq;

public class PlaywireSDKTargeting {

	private Dictionary<string, string> Targeting = new Dictionary <string, string> ();
	
	public PlaywireSDKTargeting Clear()
	{
		this.Targeting.Clear();
		return this;
	}
	
	public PlaywireSDKTargeting Add(PlaywireSDKTargeting targeting) 
    {
		if (targeting != null) 
        {
			return this.Add(targeting.Targeting);
		}
		return this;
	}
	
	public PlaywireSDKTargeting Add(Dictionary<string, string> targets) 
    {
		if (targets != null) 
        {
			var oldTargets = this.Targeting;
			var newTargets = targets; 

			this.Targeting = newTargets
            .Concat(oldTargets.Where(x=> !newTargets.Keys.Contains(x.Key)))
            .ToDictionary(x=> x.Key, x=> x.Value);
		}
		return this;
	}
	
	public PlaywireSDKTargeting Remove(string[] keys) 
    {
		if (keys == null) return this;

		foreach (var key in keys) 
        {
			this.Targeting.Remove(key);
        }			
		return this;
	}
	
	private const string ClientTagBase = "cust_cli_tag";

	public PlaywireSDKTargeting SetClientTag(string clientTag, int index) 
    {
		if (clientTag == null) return this;

		var key = String.Format("{0}{1}", PlaywireSDKTargeting.ClientTagBase, index);
		var item = new Dictionary<string, string> 
        {
			{ key, clientTag },
		};
		return this.Add(item);
	}
	
	public PlaywireSDKTargeting RemoveClientTag(int index) 
    {
		var key = String.Format("{0}{1}", PlaywireSDKTargeting.ClientTagBase, index);
		var keys = new string[1] { key };
		return this.Remove(keys);
	}
	
	public PlaywireSDKTargeting SetClientTags(string[] clientTags) {
		if (clientTags == null) return this;
		
		for (int index = 0; index < clientTags.Length; index++) 
        {
			string tag = clientTags[index];
			if (tag == null)
            {
				this.RemoveClientTag(index + 1);
			} else {
				this.SetClientTag(tag, index + 1);			
			}
		}
		
		return this;
	}

    protected static string DecodeDictionaryToJsonString(IDictionary<string, string> dictionary)
    {
        var entries = dictionary.Select(d => string.Format("\"{0}\": \"{1}\"", d.Key, string.Join(",", d.Value)));
        return "{" + string.Join(",", entries) + "}";
    }

    public override string ToString()
	{
        return DecodeDictionaryToJsonString(this.Targeting);
	}
}