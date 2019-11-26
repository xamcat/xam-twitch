# Twitch App Exploration

## Twitch
https://www.twitch.tv/ is in their own words:
>"a global community of millions who come together each day to create their own entertainment: unique, live, unpredictable, never-to-be repeated experiences created by the magical interactions of the many. With chat built into every stream, you don’t just watch on Twitch, you’re a part of the show."

## MobCAT Sample (Codename Zimmer)
The goal of the MobCAT sample is to build a Twitch app via live-coding on Twitch using the latest and greatest in Xamarin.Forms with the MobCAT Toolbox to demonstrate the thought process in using best practices for common use cases. After the app is built, we will also be profiling the app to demonstrate common profiling scenarios.

## Episodes
Sandwich episodes with TDD (unit tests, UI tests) and profiling when possible
### [1: Dean - Intro - File new proj - Setting up dummy views, shell, mobcat, CI/CD](https://www.youtube.com/watch?v=BT41EoUTKjY&t=1s)
### [2: Ben - Build API wrapper - Building out discover tab, Font Awesome and Style Triggers](https://www.youtube.com/watch?v=h3j4s9YnxPk)
### [3a: Alexey - Video player inception](https://www.youtube.com/watch?v=hP8F8P9vbVA)
### [3b: Alexey - Native iOS video player implementation](https://www.youtube.com/watch?v=lC9WTV2i2Ws)
### [3c: Alexey - Native Android video player implementation](https://www.youtube.com/watch?v=KUa9BbWwQqE)
### [4a: Sweeky - Auth Initial Setup](https://www.youtube.com/watch?v=rlcR_2xsIFo)
### [4b: Sweeky - Auth Completion + Shell Route Login Flow](https://www.youtube.com/watch?v=Bbg3wIjBK-Y)
### 4c: Sweeky - UITests? DevOps? 
### 5: Mike - Follow, image/video caching
### 6: Alex/ey - Profiling

## Wiki
Check out the wiki for more details on the episodes! https://github.com/xamcat/xam-twitch/wiki

## Design
The design mockups were created in [Figma](https://www.figma.com)
![Design](https://github.com/xamcat/xam-twitch/blob/master/Zimmer%20Design.png)

---

## MobCAT Twitch MVP Functionality

### Auth
The user will be able to login

### Following
The user can view livestreams and videos from channels they are following

### Random Stream
The user will be shown a livestream randomly from their recommendations

### Browse
The user can browse categories and livestreams

### Search
The user can search for a category or channel

### Category Details
Lists livestreams or videos of the category

### Channel Details
Shows livestream, past broadcasts, highlights, clips, and info


## Nice to Haves
### Chat (Phase 2)


## Xamarin Concepts
The Xamarin concepts and best practices that will be highlighted are
- Shell
- Cross platform mobile architecture best practices
- MobCAT toolbox usage
- Asynchronous programming
- Smart image caching and loading
- Performance optimization, memory management, and profiling
- Custom Controls for something? (like Top tabs or some button or something) (inline video)
- Manual notification for new app version (From AppCenter)
- Forms Visual (Material)
- Unit Testing
- UI Testing
- CI/CD concepts (AppCenter)
- Package creation
- Publishing just to AppCenter
- *Push notifications

* Needs a separate mechanism

---

## Twitch API
The Twitch API documentation can be found here: https://dev.twitch.tv/docs/api/

## Official Twitch App Functionality (iOS)

### Auth 
The user can log in to their Twitch account for app personalization
- Biometric Auth? (as an option)?

### Following Tab
The user can view categories and channels they are following both live and offline, get a list of recommended channels, and continue watching videos where they left off.

### Discover Tab
Contains horizontal lists of
- Live channels with automatic playback on current item
- Recommended live channels
- Recommended categories
- Recommended smaller communities
- Recommended channels
- Popular clips
- Popular videos

### Browse Tab
The user can browse by categories or live channels which can be filtered by tags.

### Livestream/Video view (Modal/Pop out)
The user can watch a livestream or video and chat with other users, cheer to support content creators with real money, watch the video in fullscreen, share a link to the video, host the channel, create a 30s clip, share the clip, follow the channel, get notifications for the channel once followed, and view options

### Livestream/Video Options (Modal)
The user can set 
- Broadcast Video & Audio Options
- Filter NSFW chat
- Hear audio only
- View chat only
- Follow broadcaster
- Subscribe to live notifications for broadcaster
They can also do the following
- Block broadcaster
- Report broadcaster
- Report playback issue

### Category Details (Push Nav)
Tapping on a category will show the details page which lists live channels of the category. The user can favorite the category, view videos and clips instead, and filter the list of content by tags

### Channel/User Details (Push Nav)
Tapping on a channel will show the channel's profile page which is similar to the user's own profile page but with a follow right nav bar item instead of the settings cog

### User Profile Page (Push Nav) (Left Nav Bar item)
The user can view their follower and view counts, go live, view their dashboard, manage their stream, view their videos, clips, info, and chat. 

### Settings (Modal) (Accessible from Profile or Notifications)
Clicking on the settings cog shows a bottom flyout with:
- Settings
- Enable light/dark theme
- Change Presence
Tapping the settings button takes them to the settings modal where they can edit the following settings:
- Account
- Preferences
- Notifications
- Security & Privacy
- Recommendations
and view the following
- Licenses
- Credits
- Terms of Service
- Community Guidelines
and also log out of their account
- Theming
    - light or dark theme (or read from OS settings?)
    - auto change to light/dark based on time of day

### Notifications (Push Nav) (Right Nav Bar item 1)
The user can see their most recent notifications and manage their notification settings

### Social (Push Nav) (Right Nav Bar item 2)
The user can access their whispers (chats) and friends

### Search (Modal) (Right Nav Bar item 3)
The user can search for 
- Live streams
- Channels
- Categories
- Videos
and view top results from each of the categories

### Caching
- caching login credentials
- caching some video content?

### Push Notifications
- Get a push/reminder if when person/channel you are following starts streaming
