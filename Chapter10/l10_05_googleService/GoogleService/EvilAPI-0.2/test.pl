use strict;
use warnings;

sub test_search
{
    use Net::Google::Search;

    my $search = Net::Google::Search->new({key => 'foo bar baz'});
    $search->query('paris');
    $search->max_results(20);
    $search->starts_at(10);


    my $results = $search->execute();
    while(my $result = $results->fetch_result())
    {
        print $result->title, ' :: ', $result->URL, "\n";
    }

    print "\n";
    print $results->searchTime, "\n";
    print $results->estimatedTotalResultsCount, "\n";
    print $results->estimateIsExact, "\n";
}

sub test_spelling
{
    use Net::Google::Spelling;

    my $spelling = Net::Google::Spelling->new({key => 'foo bar baz'});
    $spelling->phrase('new yirk city');
    
    print $spelling->suggest(), "\n";
}

sub test_cache
{
    use Net::Google::Cache;

    my $cache = Net::Google::Cache->new({key => 'foo bar baz'});
    $cache->url('http://sitening.com');
    
    print $cache->get(), "\n";
}

test_cache();
