# StoryBuilder ChangeLog

## Release 2.7.0.0

As of February 9, 2023, we're rolling out Release 2.7.0.0.

### New Features

#### ARM64 Support (#109, #481)

StoryBuilder now builds for three platforms: Intel x64 and x86, and ARM64.
This supports Windows 11 running on ARM-based systems such as Surface Pro X.

#### Improvements to Conflict Builder (#484)

We've added  additional subcategories of criminal conflucts and
expanded existing category/subcategories with more examples.

#### Problem Category (#491)
We've added a new field to the Problem class, ProblemCategory. 
This is a non-editable drop-down list (SfComboBox) which describes
the purpose of the problem in terms of story structure.

#### Unit testing / additional unit tests (#17, #492)

We've got the StoryBuilderTest executing test scripts from Test Explorer.
It's not yet running as a part of PR review prior to merge. We'll get 
that added in the next (2.8.0.0) release.

As time permits, tests need to be generated to fill in code coverage, 
especially for user interactions.

### Bug fixes

#### Re-add Listview for Scene Purpose (#478)

#### Fix label on Setting Page for Setting Summary (#488)

#### Fix error when opening files (#489)

### Ongoing and Deferred Issues

#### User manual updates (#487, #491)

Add write-up for Problem Category with screen shot.

Fix invalid markdown tag on bullet list items.

Add Next/Previous navigation.

#### Produce first newsletter (#)

We are up on MailChimp, and have content drafted for the first newsletter, 
which is the 2.6.0.0 changelog and the 2.7 roadmap. We also have additional
posts drafted, and a template with logo for the newsletter is being 
put together. 

Rather than one monthly newsletter, we'll be producing several smaller 
newsletters throughout the month: a newsletter for each new release,
and several newsletters with articles and tips.

Blog posts on storybuilder.org and the newsletters will contain similar
content.

## Release 2.6.0.0

As of January 24, 2023, we've rolled out Release 2.6.0.0.
This release was late due to a number of issues as well
as holiday and school schedules.

### New Features

#### Expand market area to all stores

We've expanded our distribution to include all
English-based countries in our  Microsoft Store
market.

(This was done in the 2.5.1.0 point release, which
fixed issues with AutoSave and the WebView2 runtime, 
used in our WebPage Story Element.)

#### Remove SfComboBox (#464) 

This change results in the replacement of SyncFusion's
SfComboBox with the Microsoft ComboBox. This will make it 
easier for developers to work with the codebase because they
won't need their own SyncFusion licenses in order to do so.

#### Back out remove SfComboBox (#477)

We rolled back the SfCombobox replacement due to problems
with the Microsoft ComboBox control; we were getting bind
failures. This is still under investigation but we note
that there appear to be quite a few open problems relating
to ComboBox on the WinUI GitHub.

#### Purpose of Scene rewrite (#457)

Purpose of Scene was also an SfComboBox, because it allows
the selection of multiple values (a scene should do more than
one thing.) It's been converted to a custome control based on
a Listview with radio buttons. Since this change doesn't
depend on the Microsoft ComboBox, we're retained it.

#### Revise Character Relationships 'Create a new relationship' (#458)

Previously, the list of character Relations (relationship types,
such as father -> son or boss -> employee) was fixed. This
doesn't work, since the number of possible relastionships
is large and dynamic. The list was made editable so that users
can add their own relationship types.

#### Implement a Print Manager for printed reports (#157)

We added a Print Manager as a part of the Print Report menu.
This allows a user to select a printer and specify its 
StoryBuilder print report options. 

As of 11/15/2022 (with WinAppSdk 1.2) the Print Manager is available and works
with Windows 11 but doesn't work with Windows 10. 
We disable the Print Manager with a status message if
the if the user is not on Windows 11. Windows 10 users
can continue to set their default printer before generating
print reports.

We'll continue to track the status of a fix for Windows 10
users.
 
#### Codebase Cleanup (#439) (ongoing)

We're continuing to work on refactorings to
improve our codebase and will continue to do so 
indefinately. 

### Bug fixes

The following bugsd were addressed in this release:

#### Update Deletion Service to catch potential errors (#470, #472)
#### Track Version in .STBX file (#469)
#### Improve filename checking (#471)
#### Add system info to log (#473)
#### Remove cached deferred write (#474)
#### Autosave fixes (#476)
#### Fix to SaveAs (#475)

## Release 2.5.1.0

Fixed some issues with Autosave
Fixed Issues when installing WebView Runtime.

## Release 2.5.0.0

As pf December 1, 2022, we rolled out Release 2.5.0.0 for general 
distribution via the Microsoft Store.

#### Revised right-click menu

The flyout menu changes distributed with Release 2.4.0.0 had an
issue which was cleaned up in a point release (2.4.1.0) and 
subsequently in this release. 

#### Codebase cleanup

We're continuing to clean up our codebase. This release includes significant
cleanup and simplification of our viewmodels, which are among our most 
complex classes. We're also making checks for clean code a part of our
Pull Request process, in ordere to improve ongoing quality. Code cleanup
will continue in the next release as well.

#### Remove Id property  

This particular code cleanup has been on our issue list for a long time,
but was repeatedly bypassed because (a) it did no harm and (b) it was
complicated. But hey, cleanup. It's done now.

#### Update to .NET 7

Updated to .NET 7 is the successor to Microsoft .NET cross-platform 
.NET framework, .NET 6. .NET 7 is primarily a performance release and
includes improvements in ARM64 support and desktop app support.

.NET 7 supports a feature, IL Trimming, which will significantly reduce
the size of applications (and thus speed up both download time and load
time at execution.) The benefits of IL Trimming. These benefits are not
yet present for packaged MSIX WinUI 3 applications like ours, but should 
be forthcoming.


### Add an 'Overview and Main Story Problem' template for new story creation

This template focuses on the Story Problem and ties the Overview node's Premise
to one Problem node by making it the Story Problem. It also adds Protagonist
and Antagonist Character nodes to the problem.

#### Master Plots should be copied as a Problem rather than Scene

This issue was listed in the 2.5 Roadmap as 'self explanitory.' It 
isn't, exactly. The MasterPlot tool will now copy a masterplot as a
Problem story node with its description and notes contained in the problem
if it has one 'scene' in its termplate, and as a Problem with series of Scene
nodes if it's one of the 'story structure' masterplots such as Three Act Play
or Hero's Journey. Previously MasterPlots wer added as series of scenes
in all cases.

#### AutoSave improvements

Fixed a possible slowdown with AutoSave
Increased the maximum time between autosaves from 30 to 60 seconds and minimum from 5 to 15 seconds.

### Remarks

Several things planned for this release didn't make it and will be rolled forward
to future releases. These include


## Release 2.4.0.0

As of Novermber 13, 2022, we've rolled out Release 2.4.0.0 for general distribution 
via the Microsoft Store. 

2.4 contains the following new/changed features:

The WebPage story element research tool introduced in Release 2.3 now has a Preferences
tab which allows you pick the search engine to use when locating the web resource you're 
looking for. 

### UI Improvements


#### Revised right-click menu

The flyout menu is a shortcut used to add new Story Elements and several other functions.
Jake's rewriten the flyout menu to make it much more user-friendly. 

#### Highlight in-process node

A StoryBuilder outline is a tree of Story Elements which are displayed in the Navigation Pane. 
Clicking (or touching) a Story Element node on the Navigation Pane displays that 
Story Element’s content in the Content Pane. A node can also be right-clicked to
display a commandbar flyout context menu. The current (clicked) node is highighted
in the Navigation Pane, but the highlight is not very visible. Right-clicking a node
momentarily highlights it but with the light theme it's almost impossible to see.

The Navigation Pane now displays the current node and the right-mouse clicked node
(used for a target for the flyout menu) using your Display Settings accent colors to 
better highlight where you are.

#### Cast List revamp

Scene Story Elements contain a Cast List control which, as its name implies,
is used to list the characters in a scene. The Cast List control is based
on a ListView and requires a convoluted set of interactions to add and
delete cast members using a second control. Weve simplified this by allowing you
to switch between a list of all characters, with a checkbox by each name to
add or remove the character to this scene's cast, or a list of just the scene's members.

#### Progress indicator

Several tasks, notably printing reports and loading installation data, may take 
a bit of time. We've added a progress indicator as an indicator that the app
hasn't frozen. 

### Default Preferences

Some Preferences data, such as the default outline and backup folder locations,
have have been updated with some defaults (including preserving previous versions' 
values) when installing StoryBuilder.

#### Codebase cleanup

Actively maintained programs tend to accumulate cruft over time. This release we've
started a process of addressing this by working through the codebase and removing
duplicate and unusued code, conforming to some newly-set naming conventions,
and making other improvements.This is a long-term process, which will continue in
future releases, but we're making progress.

#### Bug fixes:

StoryBuilder is a new product, and our number-one priority remains bug fixes and improvements.
Some specific fixes in this release include:

*Fixed a problem where the node wrapping Preference setting was not persisting.

*Fixed the app freezing when generating large reports. A progress indicator is now shown.

* Fixed a number of crashes relating to tool use and transitioning from one user action to 
another.

### Windows App SDK Api 1.2

We've updated to the latest version of the Windows App SDK (1.2).

## Release 2.3.0.0

As of October 3, 2022, we've rolled out Release 2.3.0.0 for general distribution 
via the Microsoft Store. 

2.3 has one significant new feature, the addition of researach tools. These take
the form of two new Story Element types which can be added as nodes to your outlines:
WebPage and Note nodes.

WebPage nodes are used to store links to web pages. They're implemented using the
WebView2 control, which is built on the Edge browser. A WebPage node can use search
to find a page and will persist that page's URL in your outline so that when you 
navigate to it the page is loaded and displays in the node's content area.

Note nodes are used to store text notes. They're implemented using the RichEditBox.
Major Story Element nodes (Story Overview, Problem, Character, Setting and Scene) all
have a Notes tabes which can be used to store notes about that particular Story Element,
but the new Note nodes are intended to be used to store notes about any topic  you
wish.

StoryBuilder is a new product, and our priority remains bug fixes and improvements.
2.3 is primarily a fix release, as will be future short-term releases. Some
specific fixes in this release include:

#### Implemented Single Instancing

StoryBuilder now uses Single Instancing. If the app is already open and you launch 
it again, the existing instance will be brought to the foreground rather than having
a new instance launched. While the ability to edit more than one outline at one time
has its uses, it can also cause problems. For example, if you have two instances of
StoryBuilder open and you edit the same Story Element in both instances, the changes
from one instance will overwrite the changes from the other instance. Single Instancing
prevents that from happening.

#### Codebase cleanup

Actively maintained programs tend to accumulate cruft over time. This release we've
started a process of addressing this by working through the codebase and removing
duplicate and unusued code, conforming to some newly-set naming conventions,
and making other improvements.This is a long-term process, which will continue in
future releases, but we're making progress.

#### User Manual and sample updates

We've updated the User Manual and several sample outlines to improve the documentation
and to reflect changes in the app. As with the codebase cleanup, this will be an
ongoing process. The User Manual changes are still mostly structural, and we're
well aware that line and copy edits are needed. We'll be working on those in future
releases.

#### Bug fixes:

* Fixed a bug preventing cast information from being saved.
* Fixes several issues causing progdram crashes.
* Fixed some issues relating to topics. Besides in-line topic
  data (topic/subtopic/notes), it's possible for a topic to 
  launch Notepad to display a file. This fix has that working.
* Fixed some issues with tracking changes.

https://github.com/storybuilder-org/StoryBuilder-2/compare/2.1.2.0...2.2.0.0

## Release 2.2.0.0

* As of August 31st, 2022, we've rolled out Release 2.2.0.0. We have now opened the app up to general distribution via the Microsoft Store. 
* This release has a few fixes and improvements whilst implementing new features such as the Narrative Editor.
* Optimized code
* Fixed accidental spell checking on the email box.
* Added a new menu called Narrative Tool to make editing
the narrator view easier.
* Fixed Icons on certain nodes not showing up.
* Added a prompt to open the quick start menu when opening
StoryBuilder for the first time.
* Updated the Danger Calls sample story and the tutorial in the User Manual.
* Fixed an error where the story said it was saved when it really wasn't.
* Made some minor changes to the contents of the comboboxes.
* New story overview nodes are now called the name of the story instead
of just working title.

https://github.com/storybuilder-org/StoryBuilder-2/compare/2.1.2.0...2.2.0.0

## Release 2.1.2.0

Fixed syncfusion licensing issue.

## Release 2.1.1.0

Fixed scaling issues.

Updated dependencies.

## Release 2.1.0.0

As of July 29, 2022, we've rolled out Release 2.1.0.0. This is the 
completion of a major milestone, distributing StoryBuilder 
via Windows Store direct link. 

This release has a ton of fixes, adds our privacy policy, and contains documentation improvements.
A point release, 2.1.1.0, on August 1, 2022, fixed a scaling issue we missed in 2.1.0.0. 
We allow Windows Store client installations for any Windows user who has a link to the download, from a
link through the website (https://storybuilder.org) and other channels. 

Added a roadmap

Added Autosave

Updated some combobox choices

Storybuilder will now show the changelog on an update.

Revamped Relationship layout

Updated manual

Added StoryBuilder Server support.


## Release 2.0.0.0

Fixed warning by @Rarisma in #311

Updated preferences UI

Fixed issues with Reports menu by @Rarisma in #317

Datalogging by @terrycox in #321

Fixed issues with Moving buttons

Fixed issues with generated reports by @Rarisma in #323

Updated dependencies by @Rarisma in #322

Fixed some crashes and inconsistencies in report naming by @Rarisma in #335

Make startup quicker by @Rarisma in #336

Update packages by @Rarisma in #340

Fixed CharacterName, ProblemName and SceneName control issues by @Rarisma in #341

User is now prevented from opening the add relationships menu if there arent any prospective partners
Get ready for the store by @Rarisma in #344

Fix DotEnv Requirement by @Rarisma in #346

Reverted AutoComplete controls to SyncFusionComboboxes by @Rarisma in #352

Added some new keybinds and fixes report issues relating to outertraits by @Rarisma in #355

https://github.com/storybuilder-org/StoryBuilder-2/compare/2.0.14.0...2.0.0.0

## Release 2.0.14.0

Tweak UI

Fixed logging

Fixed bugs

## Release 2.0.13.0

Fixed Numerous smaller bugs by @Rarisma in #258

Fixed crash caused by teaching tip by @terrycox in #259

Fix Dark Mode Coloring by @Rarisma in #265

Fix some fields not saving by @Rarisma in #282

Updated about pageby @Rarisma in #283

Updated search expirence by @Rarisma in #287

Fix Releationship display issue by @Rarisma in #291

Correct Setting Combobox by @terrycox in #292

Fix Logging Error by @Rarisma in #294

Added examples by @terrycox in #293

Updated Automated Release

## Release 2.0.12.0

Added privacy policy (Read it here https://github.com/storybuilder-org/StoryBuilder-2/blob/master/PRIVACY_POLICY.TXT)

Fix some issues with Cast Members

Added some tooltips

Updated repository documentation

Fixed issue with content persisting when a new story is loaded

Fixed some issues regarding dark mode

Fixed some grammar regarding search results

Added tool tip to the edit icon

Updated Scene purpose to allow multiple values

When StoryBuilder is opened, the file open menu is shown

Cleaned up code and removed unused values

Updated logging

Updated samples to fix typos and grammar

Fixed bug which would cause the report printer to make tons of reports

Renamed Literary device to Literary Technique

Fixed saveas being broken

Updated list of countrys to be much more complete

Implimented autobuilds


Made sizing better, especially for screens using scaling

Fixed issue releating to content in structure tab not saving

Fixed wording in problem page

Fixed issue with locale and season showing the wrong values

Fixed crash relating to moving certain elements

Fixed crash releating to adding cast to stories

Removed quotes and characterization aids

Fixed error caused when cancling dramatic situations

Updated dependencies

Updated File menu to show the last edited date, and the path on hover

Fixed bug which caused stock scenes to insert two nodes instead of one, and now shows default when opened.

Fixed bug which caused text to be at the right of the screen

Removed need for keys to be read through environment variables, this new system does not require the user to ddo anything

Improved logging clarity

Fixed bug which would cause crashes with teaching tips

Clicking on the save pen will now save your file





