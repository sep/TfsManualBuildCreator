# TFS Manual Build Creator

Want to create a Manual Build in TFS?

## Wait, what?

Look, sometimes you just need to integrate disparate systems, and neither system really plays well with one another.

That's where we were stuck. Our build process used [Git](http://git-scm.com/) for source control management and [Jenkins](http://jenkins-ci.org/) for builds and continuous integration... and then we needed to use [Microsoft Test Manager](http://msdn.microsoft.com/en-us/library/jj635157.aspx) (MTM) to handle test traceability (and register our automated tests).

## Oh, so you create the builds elsewhere?

Yes. All the builds are created on Jenkins, which is not Microsoft Team Build. We have the build artifacts (including assemblies) sitting on a network share after Jenkins creates them.

## So you're really just letting TFS know where the builds are?

Exactly.

## Why didn't you just create a XAML build template and--

Let me stop you there.

There's an old addage that says "XML is like violence; if it doesn't work, use more". Well, XML didn't work so hard that they had to add another letter to it ("A").

And I'm not a violent person.

## You make a persuasive argument. So how do I tell MTM about my tests?

Simply enough, comparatively.

You'll need to have a project in TFS and proper permissions to create builds.

1. Create a Build Definition in Visual Studio under your TFS project. Make sure the Trigger is set to "Manual".
1. Use the `tfs-mbc.exe` utility to create a manual build.
1. You're done.

## Can you show me an example?

With pleasure!

    ./tfs-mbc.exe                                         \
       -s https://your.tfs.server:port/tfs/CollectionPath \
       -b "Build Label-1.0.0.2345"                        \
       -p "Tfs Project"                                   \
       -d "\\NETWORKCOMPUTER\Shared\BuildHome"            \
       --build-definition "Build Definition Name"

If everything is successful, you should see the message: `Build created successfully`.

## Usage information?

If you run the executable from the command line with no parameters it will give you some nice usage information. If you're too lazy, simply read the next line. If you're too lazy for that, I don't want you using my utility.

    TFS Manual Build Creator 1.0.0.0
    Copyright (C) 2014 SEP Labs
    Released under BSD License
    Usage:
            tfs-mbc.exe -s <server> -b <build label> -d <drop folder> -p <project name>
    --build-definition <build definition>
            tfs-mbc.exe -h
    
      -s, --server          Location of TFS collection instance
                            (http[s]://server:port/path/CollectionName)
    
      -b, --build           Label applied to manual build
    
      -h, --help
    
      -p, --project-name    Name of TFS Project to create manual build against
    
      -d, --drop-folder     Location of build DLLs
    
      --build-definition    Build Definition to create manual build under
    
      --help                Display this help screen.

## Are there any prerequisites?

You'll need to install [Team Foundation Server 2012 Object Model](http://visualstudiogallery.msdn.microsoft.com/f30e5cc7-036e-449c-a541-d522299445aa), which is pretty small.

## This is super amazing and very helpful!

Thanks!

## But I have questions!

Feel free to drop a line to [the project maintainer](mailto:rmrogers@sep.com) or create an issue.

## You are handsome.

Hey. Right back at you.

## License

We use the BSD license.
