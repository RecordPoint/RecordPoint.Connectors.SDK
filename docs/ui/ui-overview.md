# Connector Configuration UI Overview

## Purpose

The Connector Configuration UI provides an user interface to allow the user to create, delete and configure their connectors. It communicates to the Eiger Management API to do this.

## Background

When Connector Configuration UI was first developed, specific UIs were created for each connector. Eg Sharepoint Online v1, FileConnect Enterprise v1 and v2, Box.

To make it easier and quicker to roll-out new connectors, connectors are based on the Connector Configuration UI Library.

The Connector Configuration UI Library provides re-usable and generic UI components that can be used to represent settings for the connector. The specification of what UI components should be rendered for a connector type is defined in the UI manifest section of connectors configuration YAML file for that connector type.

## Setting/Getting properties between UI and backend

**TODO** Explain how the UI sets/gets properties to/from backend

## Connector Configuration UI Library

The UI components available in the Connector Configuration UI Library that are used by the connectors.

**TODO:** More details will be fleshed out for the following components to cover their purpose, how to specify in the UI manifest, etc.

### Basic components

* Text field
* Text box
* Switcher
* Drop down list (combobox)
* Button (To be done)
* Radio button (To be done)
* Secret field
* Anything else?

### Container components that can contain any of the basic components

* Form
* Popup
* Grid list of values
* Link Form
* More to add...

### Feature components that are used in more than one connector

* Aggregation level
* Originating organization form
* Version level form
* Content registration
* More to add...

