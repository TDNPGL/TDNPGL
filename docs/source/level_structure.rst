Level structure
==================================
You already have project.

The project contains **lvl_main.json** as a resource, open it in any text editor. It contains text similar to
``{"objects":[],"name":"lvl_main","backColor":"#00000000"}``

 * ``objects`` - a list of objects at the level(GameObject[])
 * ``name`` - the name of the level used as a universal ID(string)
 * ``backColor`` - HEX color, shows. when background doesn't exist(HEX color as string)

An object is a thing of the GameObject class.
It's contains:
 * ``sprite`` - sprite, using on rendering(string)
 * ``animate_delay`` - time beethween frame updates(int)
 * ``z_layer`` - Z-coordinate, the lower the ``z_layer``, the later the object is rendered (that is, it sits on top of others)(int)
 * ``aabb`` - hitbox or collider of an object.(AABB)
 
    * AABB structure contains:
        * ``min`` - minimal point of AABB
        * ``max`` - maximal point of AABB
        * ``min`` and ``max`` contains ``x`` and ``y`` (int)
 * ``listeners`` - event listeners(string[]) 
 * ``multiplayer`` - if `true`, the object is updating in multiplayer on events, if `false` - not updating(bool)
