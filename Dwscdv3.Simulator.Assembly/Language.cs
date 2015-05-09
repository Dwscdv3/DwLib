using System;

namespace Dwscdv3.Simulator.Assembly {
	public class Language {
		const string[] Command = {
			"add", "sub", "mul", "div", "mod", 
			"inc", "dec", 
			"mov", 
			"cmp", "eql", "grt", "les", 
			"#def"
		};
	}
}