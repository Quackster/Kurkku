<?php
    
    // Overrides generator
    
    header('content-type: text/plain');
    
    // Check to see if there is already a texts file and when it was last
    // updated. If it was last updated over 12 hours ago then we will open
    // a database connection to update it.
    if(file_exists('external_flash_override_texts.txt') && (time() - 43200 < filemtime('external_flash_override_texts.txt')))
    {
        include_once('external_flash_override_texts.txt');       
    }

    $sql = new MySQLi('127.0.0.1', 'root', 'password', 'plusemu');

    if($sql->connect_error)
    {
        // There's a problem connecting to the database so the texts cannot
        // be updated. The outdated version will be displayed till we can
        // get a database connection.
        if(file_exists('external_flash_override_texts.txt'))
        {
            include_once('external_flash_override_texts.txt');
            exit;
        }
    }

    ob_start();
	
    if($stmt = $sql->prepare('SELECT `key`,`value` FROM `client_external_texts`'))
    {
        $stmt->execute();
        $stmt->bind_result($key, $value);

        while($stmt->fetch())
        {
            // All results are now fetched from the database so
            // we'll output them and cache everything once this
            // has finished.
            echo $key . "=" . $value . "\r\n";
        }

        $stmt->close();
    }
	
    if($stmt = $sql->prepare('SELECT `badge_code`, `badge_title`, `badge_desc` FROM `client_external_badge_texts`'))
    {
        $stmt->execute();
        $stmt->bind_result($code, $title, $description);

        while($stmt->fetch())
        {
            // All results are now fetched from the database so
            // we'll output them and cache everything once this
            // has finished.
            echo "badge_name_" . $code . "=" . $title . "\r\n";
            echo "badge_desc_" . $code . "=" . $description . "\r\n";
        }

        $stmt->close();
    }

    $sql->close();

    // The above has grabbed the badge names and descriptions from the database
    // and outputted them to the page. Now we need to write them to the cache
    // so we aren't opening a database connection each time this file is requested.
    $file = fopen('external_flash_override_texts.txt', 'w');

    fwrite($file, ob_get_contents());
    fclose($file);

    ob_end_flush();