# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.3.0]

### Added

- added a `Match()` extension method on the Result type to run logic based on the result status.
- added `Exception` property to ResultError.
- added new extension methods on the Result type to attach errors.

### Changed

- removed `Type` property from ResultError.
- updated some comments.

## [1.2.1]

### Added

- added a check if the `exception.Data` key is a string than we can add it to the list of meta-data.

## [1.2.0]

### Changed

- changed ResultError type to be a struct.
- changed name-space from Result.Net to ResultNet.

## [1.1.0]

### Added

- added a function `As()` to cast exception to given type.
- add an extension method `FailedBecause()` to check if the result has failed because of a given code.
- added the `Throw()` extension method over the Exception class type.

### Changed

- updated some comments.

## [1.0.0]

the initial release
