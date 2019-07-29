#REMEMBER TO SETUP PIPELINE VARIABLE
# TwitchAPIKey

if [ ! -n $(TwitchAPIKey) ]
then
    echo "You need define the TwitchAPIKey variable in VSTS"
    exit
fi

#PATH TO CONSTANTS FILE
APP_CONSTANT_FILE=<path to Constants.cs >

if [ -e "$APP_CONSTANT_FILE" ]
then
    echo "Updating App Secret Values to TwitchAPIKey in Constants.cs"
    sed -i '' 's#TwitchAPIKey = "[a-z:./\\_]*"#TwitchAPIKey = "$(TwitchAPIKey)"#' $APP_CONSTANT_FILE

    echo "File content:"
    cat $APP_CONSTANT_FILE
fi