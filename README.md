# What is Northwood?

Northwood project is a WPF application designed to be the simplest one for digital design entry for educational purposes. Northwood is designed to be a lightweight tool that you may use in seconds. We approach zero configuration time, template based creation, modern UI, touch friendly, stylus friendly, extensible via plugins, FPGA vendor independent.

# Why Northwood?

As a lecturer in Computer Architecture, I have found my students ramping an 80° learning curve when I try to hit practical experience over solid foundation. This is a tool availability issue. Here I list some considerations to get Northwood in my mind (2014).

- Aldec ActiveHDL is a well stablished and professional tool for digital design, but its scope is located to truly medium to large projects. It requires download more 1GB to install. Even to do basic simulation and even on Student version.
- Mentor Graphics ModelSim? is 66MB aprox when its Altera-flavored. Its features are for professional workstation but this package lacks block diagraming tools and intuitive interface, it is essential for newbie engineer. It has so much TCL/Tk quirks.
- GNU Tools many GNU tools are not simple to learn, there are a heavy text-based. It would be very non motivational for newbies. But these tools are a great supporting foundation to build on.

# Do you have a master plan? Roadmap

Northwood will be different because we plan a modern (2014) feature set. Yes, you are right, this is not about competition with well stablished tools.

- Schematic will be simple. Only simple features will be implemented. Its interface will be inspired in Altium Designer interface.
- Font sizes should be trimmed with touch screen and stylus in mind. Yes, stylus may be a simple but wonderful tool for engineering CAD.
- Design will be converted to HDL on-the-fly. Later you may target your favorite FPGA vendor.
- HDL models will be compiled to native executable using a GNU tool, simulation will be a simple execution and wrapping of it.
- Template based. Just start a new design using templates in seconds.
- Extensible via plugins. NuGet? packages will be the back end.
- UIX with Ribbon.
- NO MORE need to download multi-giga-byte packages for Xilinx ISE, Xilinx Vivado, Altera Quartus, etc. We need plugins and people offering FPGA synthesis as a WebService?. Newbies will be very lucky.
- Board-aware wizard templates for rapid proof of concepts prototyping. Just pins assigments pre-listed and a fancy picture of our beloved favorite board.
- Using Northwood, you should get a basic design working on FPGA in a few seconds.

# Not planned

This tool is not targeting to multi language simulation. When we get a stable release may be we approach a full cross description language development, but it is not a real goal in this time.
