//
//Ping Utilities (Script_PingUtils)
//By Dominoes (37977), help from Blockland Content Creators ( https://discord.gg/r6ERkqy ), ping scoring script donated by ImPanda (30233)
//-----
//Comments next to many lines are for intrigued code-doers, if you don't like them safely ignore them
//

$PingScoringEnabled = false; //setting variable for the function

function ping() //stating the initial ping function
{
	if(isEventPending($pingSchedule))
		cancel($pingSchedule); //using this command in console disables the function, used later on

	for(%i = 0; %i < clientGroup.GetCount(); %i++)
		clientGroup.GetObject(%i).Score = clientGroup.GetObject(%i).GetPing(); //gets a user's score, then sets it to that user's ping

	$pingSchedule = schedule(100, 0, ping); //schedules the function so it loops
}

function serverCmdTogglePingScore(%client) //server command to toggle above function
{
	if(%client.isAdmin) //checks if user is admin
	{
		if($PingScoringEnabled == false) //checks if the variable at the top is set to false. notice the == instead of =
		{
			ping();
			
			$PingScoringEnabled = true; //modifies the variable above
		
			messageClient(%client,'',"\c6Ping score function \c2enabled\c6.");
			messageall('MsgAdminForce', "\c2" @ %client.name SPC "\c6has enabled ping scores.");
		}
		else
		{
			cancel($pingSchedule);
			
			$PingScoringEnabled = false;
			
			messageClient(%client,'',"\c6Ping score function \c0disabled\c6.");
			messageall('MsgAdminForce', "\c2" @ %client.name SPC "\c6has disabled ping scores.");
		}
	}
	else
		messageClient(%client,'',"\c6You do not have access to this command.");
}

function serverCmdGetPing(%client, %target)
{
	if(%client.isAdmin)
	{
		%target = findclientbyname(%target); //sets a variable, %target, to the object's name put in after the initial /getping
		if(isObject(%target)) //determines if the target object is a player
		{
			%ping = %target.getPing(); //gets the target's ping, sets it to the variable stated
			announce("\c2" @ %client.name SPC "\c6checked\c2" SPC %target.name @ "\c6's ping:\c5" SPC %ping @ "ms");
		}
		else
		{
			messageClient(%client,'',"\c6Invalid player.");
		}
	}
	else
	{
		messageClient(%client,'',"\c6You do not have permission to use this command.");
	}
}
