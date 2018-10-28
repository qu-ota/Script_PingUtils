$PingScoringEnabled = false;

function serverCmdTogglePingScore(%client)
{
	if(%client.isAdmin)
	{
		if($PingScoringEnabled == false)
		{
			function ping()
			{
				if(isEventPending($pingSchedule))
					cancel($pingSchedule);
			
				for(%i = 0; %i < clientGroup.GetCount(); %i++)
					clientGroup.GetObject(%i).Score = clientGroup.GetObject(%i).GetPing();
			
				$pingSchedule = schedule(100, 0, ping);
			}
			
			ping();
			
			$PingScoringEnabled = true;
		
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
		%target = findclientbyname(%target);
		if(isObject(%target))
		{
			%ping = %target.getPing();
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