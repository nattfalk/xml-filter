# xml-filter

![](https://github.com/nattfalk/xml-filter/workflows/.NET%20Core/badge.svg)

Extremly simple .NET Core command-line tool for filtering XML files using XPath

## Prerequisites

- .NET CORE 3

## Installing

```
$ git clone https://github.com/nattfalk/xml-filter.git
$ cd xml-filter
$ dotnet build
```

## Usage

```
xml-filter [in-file] [out-file] [base-node] [xpath-filter]
```

## Examples

```
> xml-filter c:\xml\input.xml c:\xml\output.xml //element [value1='abc']
```

Filters the input.xml file and removes all "element"-nodes not matching the xpath-filter.

```xml
<!-- input.xml -->
<?xml version="1.0" encoding="UTF-8"?>
<root-node>
  <element>
    <value1>abc</value1>
    <value2>abc123</value2>
  </element>
  <element>
    <value1>cde</value1>
    <value2>cde123</value2>
  </element>
</root-node>
```

```xml
<!-- output.xml after filtration -->
<?xml version="1.0" encoding="UTF-8"?>
<root-node>
  <element>
    <value1>abc</value1>
    <value2>abc123</value2>
  </element>
</root-node>
```

## Authors

- **Michael Nattfalk** (michaelnattfalk@gmail.com)

## License

[![License](http://img.shields.io/:license-mit-blue.svg?style=flat-square)](http://badges.mit-license.org)

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details
