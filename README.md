UnityHexGrid
============

Components and utilities for Unity3D for working with hexagonal maps.

UnityHexGrid is heavily based on Amit Patel’s code found at http://www.redblobgames.com/grids/hexagons/.

For now there are two Unity components:

### HexMap

HexMap provides the actual hexagonal grid. Just add the component to an empty GameObject and you see a hex map drawn as a gizmo.

**Properties**
* *Orientation* Decides which way the hexes are pointing. Horizontal the hexes point upwards, Vertical sideways.
* *Scale* Scales the whole map by the amount given.
* *Size* The size of the map in hexes. The actual size depends on the selected shape.
* *Shape* The map can have three shapes: Trapeze, Triangle and Hexagonal.
 
### HexTile

The HexTile component is added to the actual game pieces, that are placed on the map. It provides coordinates in hex space, that snap the GameObject to the corresponding hex on the map. Where the object is placed depends on the shape and orientation of the map.

### Additional classes

* *Cube* has functions for working with cube coordinates.
* *ScreenCoordinate* functions to work with screen coordinates.
* *HexGrid* helper functions to convert between coordinate systems.

See Amid Patel's page for more information.
