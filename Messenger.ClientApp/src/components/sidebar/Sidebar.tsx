import { DesktopSidebar } from './DesktopSidebar';
import { MobileFooter } from './MobileFooter';
import { useAppSelector } from '../../hooks';
import { FC } from 'react';

type SidebarProps = {
    children: React.ReactNode
}

export const Sidebar: FC<SidebarProps> = ({ children }) => {
  const currentUser = useAppSelector(x => x.userState);

  return (
    <div className="h-full">
      <DesktopSidebar currentUser={currentUser.user!} />
      <MobileFooter />
      <main className="lg:pl-20 h-full">
        {children}
      </main>
    </div>
  )
}