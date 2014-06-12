assumptions:
any map is x*x*x big (so equal dimensions)
the first line is of the format [size = #] where # represents the x for the map size

every row is assumed black and/or empty if not filled in (i.e. {} == {0 0 0 0 0 0 0 0 0 0})
every level is assumed black and/or empty if not filled in (i.e. [] == [{} {} {} {} {} {}]
there is no space  sensitivity and no endline sensitivity (except for first line).
only lowercase alphabetical and numbers are accepted.


colors allowed are:
r: red
b: blue
y: yellow

p: purple
g: green
o: orange

rgb(0-255,0-255,0-255): give any rgb value not preset (this is for possible future use)

0: null/black