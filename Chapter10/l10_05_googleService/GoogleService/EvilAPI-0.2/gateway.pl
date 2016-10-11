#!/usr/bin/perl

use warnings;
use strict;

use GoogleSearch;
use SOAP::Transport::HTTP;

# Why is this on_action() call necessary? What's going on? Grrr...
SOAP::Transport::HTTP::CGI->on_action(sub{})->dispatch_to('GoogleSearch')->handle();
