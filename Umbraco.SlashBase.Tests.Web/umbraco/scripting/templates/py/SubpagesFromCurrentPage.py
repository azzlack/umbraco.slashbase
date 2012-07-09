﻿from umbraco.presentation.nodeFactory import Node
from umbraco import library

#set the node id you would like to fetch pages from here
#you can also set it as a macro property with the alias 'nodeId' instead


result = "<ul>"

for childNode in currentPage.Children:
  result += "<li><a href='" + library.NiceUrl(childNode.Id) +  "'>" + childNode.Name + "</a></li>"
  
result += "</ul>"
  
print result
