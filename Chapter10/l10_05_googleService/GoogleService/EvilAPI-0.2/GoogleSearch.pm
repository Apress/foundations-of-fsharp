package GoogleSearch;

use strict;
use warnings;

use CGI qw(escapeHTML);
use LWP::UserAgent;

################################################################################
### Handle incoming SOAP requests
################################################################################

sub doGetCachedPage
{
    my $type = shift();
    my $key = shift();
    
    return google_cache(@_);
}

sub doSpellingSuggestion
{
    my $type = shift();
    my $key = shift();
    
    return google_spelling(@_);
}

sub doGoogleSearch
{
    my $type = shift();
    my $key = shift();
    
    return google_search(@_);
}

################################################################################
### Common code
################################################################################
 
my $ua = LWP::UserAgent->new();
$ua->agent('Mozilla/5.0 (X11; U; Linux i686; en-US; rv:1.8.1) Gecko/20061010 Firefox/2.0');

sub make_request
{
    my @args;
    for my $arg (qw(q start filter restrict safe lr))
    {
        last unless @_;
        
        my $val = shift();
        if($arg eq 'safe')
        {
            $val = $val ? 'active': 'off';
        }

        push @args, "$arg=$val";
    }
    
    my $url = 'http://www.google.com/search?hl=en&'.join('&', @args);
    
    my $response = $ua->get($url);
    until($response->is_success)
    {
        $response = $ua->get($url);
    }
    return $response->content;
}

sub strip_html
{
    my($str) = @_;
    $str =~ s/<[\/]?[bi]>//g if $str;
    return $str;
}

################################################################################
### doGoogleSearch()
################################################################################

my $is_result = qr/<div class=g><!--m--><h2 class=r>(.+?)<\/nobr><\/font><!--n--><\/td><\/tr><\/table><\/div>(.+)$/s;
my $get_snippet = qr/<font size=-1>(.+?)<br>(.+)/s;
my $get_size = qr/ - (\d+k) - (.+)/s;
my $get_url_title = qr/^<a class=l href="(.+?)">(.+?)<\/a>(.+)/s;
my $translatable = qr/<font size=-1> - \[ <a.+? \]<\/font>(.+)/;

sub parse_results
{
    my($data) = @_;
    
    my @results;
    while($data && $data =~ $is_result)
    {
        my($res, $rest) = ($1, $2);
    
        $res =~ $get_url_title;
        
        my $url = $1;
        my $title = $2;
        $res = $3;
        my $snippet;
        my $size;
        
        # Deal with pages where translations are available
        if($res =~ $translatable)
        {
            $res = $1;
        }
        
        # Some results may not have a snippet or size
        if($res =~ $get_snippet)
        {
            $snippet = $1;
            $res = $2;
        }
        if($res =~ $get_size)
        {
            $size = $1;
            $res = $2; 
        }

        push @results, {title      => escapeHTML(strip_html($title)),
                        URL        => escapeHTML(strip_html($url)),
                        snippet    => escapeHTML(strip_html($snippet)),
                        cachedSize => $size};
                        
        $data = $rest;
    }
    return @results;
}

my $get_time = qr/\(<b>(.+?)<\/b> seconds\)/;
my $get_result_count = qr(Results <b>\d+</b> - <b>\d+</b> of(?: about)? <b>(.+?)</b>);

sub google_search
{
    my $start = $_[1];
    my $max_results = splice(@_, 2, 1);
    
    my $data = make_request(@_);
    my @results = parse_results($data);
    
    $data =~ /$get_result_count/;
    my $estimate = $1 || 0;
    $estimate =~ s/,//g;
    
    $data =~ /$get_time/;
    my $time = $1;
    
    $max_results = @results if @results < $max_results;
    
    return {resultElements => [@results[0..($max_results - 1)]],
            startIndex => $start,
            endIndex => $start + $max_results,
            estimatedTotalResultsCount => $estimate,
            estimateIsExact => 0,
            searchTime => $time};
}

################################################################################
### doSpellingSuggestion()
################################################################################

my $fix_spelling = qr(Did you mean: </font><a href=.+? class=p>(.+?)</a>);

sub google_spelling
{
    my($phrase) = @_;
    
    my $data = make_request($phrase);
    
    # If there's no spelling suggestion, we assume the input is the right
    #  spelling.
    if($data =~ $fix_spelling)
    {
        return strip_html($1);
    }
    return $phrase;
}

################################################################################
### doGetCachedPage()
################################################################################

my $get_cache_url = qr(<a class=fl href="(.+)">Cached</a>);

# This works by searching Google for the input URL, then regexing out the first
#  "Cached" link and following that.
sub google_cache
{
    my($url) = @_;
    
    my $data = make_request($url);
    
    if($data =~ $get_cache_url)
    {
        return escapeHTML($ua->get($1)->content);
    }
    return undef;
}

return 1;
