# xml-filter

![](https://github.com/nattfalk/xml-filter/workflows/.NET%20Core/badge.svg)

Simple .NET Core command-line tool for filtering XML files using XPath

## Prerequisites

- .NET CORE 3

## Installing

```
> git clone https://github.com/nattfalk/xml-filter.git
> cd xml-filter
> dotnet build
```

## Usage

```
xml-filter -i [input-file] -x [xpath-filter] [additional options]

  -i, --input       Required. Input XML file
  -x, --xpath       Required. XPath filter used to filter the elements
  -o, --output      Output XML file
  -c, --contents    Displays contents from given number of elements after applied filter
```

## Examples

### Example 1

Apply filter and display number of filtered elements

```
> xml-filter -i testfiles\cd_catalog.xml -x /CATALOG/CD[COUNTRY='EU']
```

Results in

```
Filter resulted in 5 of totally 26 elements.
```

### Example 2

Apply filter and display contents of first 2 filtered elements

```
> xml-filter -i testfiles\cd_catalog.xml -x /CATALOG/CD[COUNTRY='EU'] -c 2
```

Results in

```xml
Filter resulted in 5 of totally 26 elements.

Displaying contents of 2 elements
---------------------------------
<CD>
  <TITLE>Eros</TITLE>
  <ARTIST>Eros Ramazzotti</ARTIST>
  <COUNTRY>EU</COUNTRY>
  <COMPANY>BMG</COMPANY>
  <PRICE>9.90</PRICE>
  <YEAR>1997</YEAR>
</CD>
<CD>
  <TITLE>Romanza</TITLE>
  <ARTIST>Andrea Bocelli</ARTIST>
  <COUNTRY>EU</COUNTRY>
  <COMPANY>Polydor</COMPANY>
  <PRICE>10.80</PRICE>
  <YEAR>1996</YEAR>
</CD>
```

### Example 3

Apply filter and save the filtered elements to file

```
> xml-filter -i testfiles\cd_catalog.xml -o result.xml -x /CATALOG/CD[COUNTRY='EU']
```

Results in

```
Filter resulted in 5 of totally 26 elements.
'test.xml' saved!
```

## Authors

- **Michael Nattfalk** (michaelnattfalk@gmail.com)

## License

[![License](http://img.shields.io/:license-mit-blue.svg?style=flat-square)](http://badges.mit-license.org)

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details
